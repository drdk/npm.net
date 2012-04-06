namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The class implements a wrapper for NPM commands
    /// </summary>
    internal class NpmApi : INpmApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmApi" /> class.
        /// </summary>
        /// <param name="wd">Working directory for project</param>
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
        public NpmApi(NpmFactory factory, string wd, string registry)
        {
            this.Initialize(factory, wd, registry);
        }

         /// <summary>
        /// Gets the NPM version string
        /// </summary>
        public string NpmVersion { get; private set; }

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
        public string GetInstalledVersion()
        {
            string version;
            string err;
            int rc = this.Client.Execute("--version", null, out version, out err);
            if (rc == 0)
            {
                return version.Trim();
            }

            // TODO throw exception if unexpected response
            return null;
        }

        /// <summary>
        /// Get installed modules in project. Wraps 'npm list'
        /// </summary>
        /// <returns>installed package</returns>
        public INpmInstalledPackage List()
        {
            string output = null;
            string err;


            int rc = this.Client.Execute("list", null, out output, out err);
            if (rc == 0)
            {
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
        public INpmRemotePackage View(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is required");
            }

            string output = null;
            string err;

            int rc = this.Client.Execute("view", name, out output, out err);
            if (rc == 0)
            {
                return this.Serializer.FromView(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Search for npm packages in repository. Wraps 'npm search term'
        /// </summary>
        /// <param name="searchTerms">words to use in search</param>
        /// <returns>enumerable set of matching packages</returns>
        public IEnumerable<INpmSearchResultPackage> Search(string searchTerms)
        {
            string output = null;
            string err;
            if (searchTerms == null)
            {
                // search for all
                searchTerms = "/.*";
            }

            int rc = this.Client.Execute("search", searchTerms, out output, out err);
            if (rc == 0)
            {
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
        public IEnumerable<INpmPackage> Install(INpmPackage package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            if (string.IsNullOrWhiteSpace(package.Name))
            {
                throw new ArgumentException("package name is required");
            }

            string output = null;
            string err;
            string args;
            if (!string.IsNullOrWhiteSpace(package.Version))
            {
                args = package.Name + "@" + package.Version;
            }
            else
            {
                args = package.Name;
            }

            int rc = this.Client.Execute("install", args, out output, out err);
            if (rc == 0)
            {
                return this.Serializer.FromInstall(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Get outdated or missing dependencies. Wraps 'npm outdated'
        /// </summary>
        /// <returns>enumerable set of packages needing updates</returns>
        public IEnumerable<INpmPackageDependency> Outdated()
        {
            string output = null;
            string err;


            int rc = this.Client.Execute("outdated", null, out output, out err);
            if (rc == 0)
            {
                return this.Serializer.FromOutdatedDependency(output);
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Check if dependency is outdated. Wraps 'npm outdated name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>npm package with newer version</returns>
        public INpmPackageDependency Outdated(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is required");
            }

            string output = null;
            string err;

            int rc = this.Client.Execute("outdated", name, out output, out err);
            if (rc == 0)
            {
                IEnumerable<INpmPackageDependency> packages = this.Serializer.FromOutdatedDependency(output);
                if (packages != null)
                {
                    return packages.First<INpmPackageDependency>();
                }
            }

            // TODO handle unexpected response
            return null;
        }

        /// <summary>
        /// Update named package. Wraps 'npm update name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        public bool Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is required");
            }

            string output = null;
            string err;

            int rc = this.Client.Execute("update", name, out output, out err);
            if (rc == 0)
            {
                return true;
            }

            // TODO handle unexpected response
            return false;
        }

        /// <summary>
        /// Uninstall named package. Wraps 'npm uninstall name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        public bool Uninstall(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is required");
            }

            string output = null;
            string err;

            int rc = this.Client.Execute("uninstall", name, out output, out err);
            if (rc == 0)
            {
                return true;
            }

            // TODO handle unexpected response
            return false;
        }

        /// <summary>
        /// Common initializtion for constructors
        /// </summary>
        /// <param name="factory">NpmFactory class</param>
        /// <param name="wd">working directory for project</param>
        /// <param name="registry">URL for remote registry</param>
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
            }
        }
    }
}
