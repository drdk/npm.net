using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webmatrix_Npm
{
    internal class NpmApi : INpmApi
    {
        /// <summary>
        /// Get npm version. Wraps 'npm --version'
        /// </summary>
        /// <returns>version string</returns>
        public string GetInstalledVersion()
        {
            string version;
            string err;
            int rc = Client.Execute("--version", null, out version, out err);
            if (rc == 0)
            {
                return version.Trim();
            }
            // TODO throw exception if unexpected response
            return null;
        }

        /// <summary>
        /// Get installed modules n project. Wraps 'npm list'
        /// </summary>
        /// <returns>enumerable set of installed packages</returns>
        public IEnumerable<INpmInstalledPackage> List()
        {
            return null;
        }

        /// <summary>
        /// Get properties of package in repository. Wraps 'npm view name'
        /// </summary>
        /// <param name="name">package name</param>
        /// <returns>NpmRemotePackage properties</returns>
        public INpmRemotePackage View(string name)
        {
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
            int rc = Client.Execute("search", searchTerms, out output, out err);
            if (rc == 0 && !String.IsNullOrWhiteSpace(output))
            {
                IEnumerable<INpmSearchResultPackage> found = Serializer.FromSearchResult(output);
                return found;
            }
            // TODO throw exception if unexpected response
            return null;
        }

        /// <summary>
        /// Install a npm package. Wraps 'npm install name'
        /// </summary>
        /// <param name="package">name and version to install</param>
        /// <returns>true or false</returns>
        public bool Install(INpmPackage package)
        {
            return false;
        }

        /// <summary>
        /// Get outdated or missing dependencies. Wraps 'npm outdated'
        /// </summary>
        /// <returns>enumerable set of packages needing updates</returns>
        public IEnumerable<INpmPackage> Outdated()
        {
            return null;
        }

        /// <summary>
        /// Check if dependency is outdated. Wraps 'npm outdated name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>npm package with newer version</returns>
        public INpmPackage Outdated(string name)
        {
            return null;
        }

        /// <summary>
        /// Update named package. Wraps 'npm update name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        public bool Update(string name)
        {
            return false;
        }

        /// <summary>
        /// Uninstall named package. Wraps 'npm uninstall name'
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Uninstall(string name)
        {
            return false;
        }

        /// <summary>
        /// Constructor taking working directory path
        /// </summary>
        /// <param name="wd">working directory for project</param>
        public NpmApi(string wd)
        {
            NpmFactory factory = new NpmFactory();
            Initialize(factory, wd, null);
        }

        /// <summary>
        /// Constructor taking working directory path and remote registry URL
        /// </summary>
        /// <param name="wd">working directory for project</param>
        /// <param name="registry">URL for remote registry</param>
        public NpmApi(string wd, string registry)
        {
            NpmFactory factory = new NpmFactory();
            Initialize(factory, wd, registry);
        }

        /// <summary>
        /// Constructor to customize client and serializer
        /// </summary>
        /// <param name="factory">NpmFactory class</param>
        /// <param name="wd">working directory for project</param>
        /// <param name="registry">URL for remote registry</param>
        public NpmApi(NpmFactory factory, string wd, string registry)
        {
            Initialize(factory, wd, registry);
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
            Client = factory.GetClient(null);

            NpmVersion = GetInstalledVersion();
            // now see if there is a better client
            Client = factory.GetClient(NpmVersion);
            Serializer = factory.GetSerialize(NpmVersion);

            if (!String.IsNullOrWhiteSpace(registry))
            {
                Client.Registry = registry;
            }
            if (!String.IsNullOrWhiteSpace(wd))
            {
                Client.WorkingDirectory = wd;
            }
        }

        /// <summary>
        /// Cient being used to execute npm
        /// </summary>
        private INpmClient Client { get; set; }

        /// <summary>
        /// Serializer used to convert npm output to objects
        /// </summary>
        private INpmSerialize Serializer { get; set; }

        /// <summary>
        /// NPM version string
        /// </summary>
        public string NpmVersion { get; private set; }

    }
}
