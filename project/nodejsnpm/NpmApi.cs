// -----------------------------------------------------------------------
// <copyright file="NpmApi.cs" company="Microsoft">
// Class for npm package manager low level API
// </copyright>
// -----------------------------------------------------------------------

namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;

    /// <summary>
    /// The class implements a wrapper for NPM commands
    /// </summary>
    [SecurityCritical]
    public class NpmApi : INpmApi
    {
        /// <summary>
        /// Working directory root
        /// </summary>
        private string rootWorkingDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmApi" /> class.
        /// </summary>
        /// <param name="wd">Working directory for project</param>
        [SecurityCritical]
        public NpmApi(string wd)
        {
            NpmFactory factory = new NpmFactory();
            this.Initialize(factory, wd, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmApi" /> class.
        /// </summary>
        /// <param name="wd">Working directory for project</param>
        /// <param name="registry">URL for remote registry</param>
        [SecurityCritical]
        public NpmApi(string wd, string registry)
        {
            NpmFactory factory = new NpmFactory();
            this.Initialize(factory, wd, registry);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmApi" /> class.
        /// The factory controls which NpmClient and NpmSerialize is used.
        /// </summary>
        /// <param name="factory">NpmFactory class</param>
        /// <param name="wd">Working directory for project</param>
        /// <param name="registry">URL for remote registry</param>
        [SecurityCritical]
        public NpmApi(NpmFactory factory, string wd, string registry)
        {
            this.Initialize(factory, wd, registry);
        }

         /// <summary>
        /// Gets the NPM version string  
        /// </summary>
        public string NpmVersion { get; private set; }

        /// <summary>
        /// Gets the INpmClient interface being used
        /// </summary>
        public INpmClient NpmClient
        {
            [SecurityCritical]
            get
            {
                return this.Client;
            }
        }

        /// <summary>
        /// Gets or sets the Cient being used to execute npm
        /// </summary>
        private INpmClient Client { get; set; }

        /// <summary>
        /// Gets or sets the Serializer used to convert npm output to objects
        /// </summary>
        private INpmSerialize Serializer { get; set; }

       /// <summary>
        /// Get npm version. Wraps 'npm --version'
        /// </summary>
        /// <returns>version string</returns>
        [SecurityCritical]
        public string GetInstalledVersion()
        {
            int rc = this.Client.Execute("--version", null);
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return output.Trim();
            }

            // TODO throw exception if unexpected response
            return null;
        }

        /// <summary>
        /// Set working directory for dependency.
        /// </summary>
        /// <param name="dependency">Dependency path or null for root</param>
        /// <remarks>Use '/' for multiple level dependency</remarks>
        [SecurityCritical]
        public void SetDependencyDirectory(string dependency)
        {
            if (dependency == null)
            {
                this.Client.WorkingDirectory = this.rootWorkingDirectory;
            }
            else
            {
                string path = this.ConvertDependToPath(dependency);
                this.Client.WorkingDirectory = path;
            }
        }

        /// <summary>
        /// Change the working directory
        /// </summary>
        /// <param name="path">Full path</param>
        [SecurityCritical]
        public void SetWorkingDirectory(string path)
        {
            this.Client.WorkingDirectory = path;
            this.rootWorkingDirectory = path;
        }

        /// <summary>
        /// Get installed modules in project. Wraps 'npm list'
        /// </summary>
        /// <returns>enumerable NpmInstalledPackage properties</returns>
        [SecurityCritical]
        public IEnumerable<INpmInstalledPackage> List()
        {
            int rc = this.Client.Execute("list", "--json");
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromListInstalled(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Get properties of package in repository. Wraps 'npm view name'
        /// </summary>
        /// <param name="name">package name</param>
        /// <returns>NpmRemotePackage properties</returns>
        [SecurityCritical]
        public INpmRemotePackage View(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is required");
            }

            int rc = this.Client.Execute("view", name);
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromView(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Search for npm packages in repository. Wraps 'npm search term'
        /// </summary>
        /// <param name="searchTerms">words to use in search or null for all</param>
        /// <returns>enumerable set of matching packages</returns>
        [SecurityCritical]
        public IEnumerable<INpmSearchResultPackage> Search(string searchTerms)
        {
            if (searchTerms == null)
            {
                // search for all
                searchTerms = "/.*";
            }

            int rc = this.Client.Execute("search", searchTerms);
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromSearchResult(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Install a npm package. Wraps 'npm install name'
        /// </summary>
        /// <param name="package">name and version to install</param>
        /// <returns>enumerable list of packages</returns>
        [SecurityCritical]
        public IEnumerable<INpmInstalledPackage> Install(INpmPackage package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            if (string.IsNullOrWhiteSpace(package.Name))
            {
                throw new ArgumentException("package name is required");
            }

            string args;
            if (!string.IsNullOrWhiteSpace(package.Version))
            {
                args = package.Name + "@" + package.Version;
            }
            else
            {
                args = package.Name;
            }

            int rc = this.Client.Execute("install", args + " --json");
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromInstall(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Get outdated or missing dependencies. Wraps 'npm outdated'
        /// </summary>
        /// <returns>enumerable set of packages needing updates</returns>
        [SecurityCritical]
        public IEnumerable<INpmPackageDependency> Outdated()
        {
            int rc = this.Client.Execute("outdated", null);
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromOutdatedDependency(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Check if dependency is outdated. Wraps 'npm outdated name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>enumerable set of packages needing updates</returns>
        [SecurityCritical]
        public IEnumerable<INpmPackageDependency> Outdated(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is required");
            }

            int rc = this.Client.Execute("outdated", name);
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromOutdatedDependency(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Update named package. Wraps 'npm update name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>enumerable list of updated packages</returns>
        [SecurityCritical]
        public IEnumerable<INpmInstalledPackage> Update(string name)
        {
            string args;
            if (string.IsNullOrWhiteSpace(name))
            {
                args = string.Empty;
            }
            else
            {
                args = name;
            }

            int rc = this.Client.Execute("update", args + " --json");
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromInstall(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Uninstall named package. Wraps 'npm uninstall name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        [SecurityCritical]
        public bool Uninstall(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is required");
            }

            int rc = this.Client.Execute("uninstall", name);
            if (rc == 0)
            {
                return true;
            }

            // TODO handle unexpected response
            return false;
        }

        /// <summary>
        /// Check if package is installed. Wraps 'npm list' and looks for match
        /// </summary>
        /// <param name="package">name and version to install</param>
        /// <returns>NpmInstalledPackage or null</returns>
        [SecurityCritical]
        public INpmInstalledPackage TestInstalled(INpmPackage package)
        {
            int rc = this.Client.Execute("list", "--json");
            if (rc == 0)
            {
                string output = this.Client.LastExecuteOutput;
                return this.Serializer.FromListMatchInstalled(output, package);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Common initialization for constructors
        /// </summary>
        /// <param name="factory">NpmFactory class</param>
        /// <param name="wd">working directory for project</param>
        /// <param name="registry">URL for remote registry</param>
        [SecurityCritical]
        private void Initialize(NpmFactory factory, string wd, string registry)
        {
            // first get default client
            this.Client = factory.GetClient(null);

            this.NpmVersion = this.GetInstalledVersion();

            // now see if there is a better client
            this.Client = factory.GetClient(this.NpmVersion);
            this.Serializer = factory.GetSerialize(this.NpmVersion);

            if (!string.IsNullOrWhiteSpace(registry))
            {
                this.Client.Registry = registry;
            }

            if (!string.IsNullOrWhiteSpace(wd))
            {
                this.Client.WorkingDirectory = wd;
                this.rootWorkingDirectory = wd;
            }
        }

        /// <summary>
        /// Build absolute path for specified dependency
        /// </summary>
        /// <param name="depends">dependency name</param>
        /// <returns>Absolute path based on working directory</returns>
        [SecurityCritical]
        private string ConvertDependToPath(string depends)
        {
            string relative = NpmSerialize.ConvertDependToPath(depends);
            if (relative != null)
            {
                string absolute = Path.Combine(this.rootWorkingDirectory, relative.Replace('/', '\\'));

                return absolute;
            }
            else
            {
                return this.rootWorkingDirectory;
            }
        }
    }
}
