namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// High level class to manage NPM installation
    /// </summary>
    internal class NpmPackageManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmPackageManager" /> class.
        /// </summary>
        /// <param name="wd">current project directory</param>
        public NpmPackageManager(string wd)
        {
            this.ApiClient = new NpmApi(wd);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmPackageManager" /> class.
        /// </summary>
        /// <param name="wd">current project directory</param>
        /// <param name="registry">registry URL if not using default</param>
        public NpmPackageManager(string wd, string registry)
        {
            this.ApiClient = new NpmApi(wd, registry);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmPackageManager" /> class.
        /// </summary>
        /// <param name="factory">NpmFactory class</param>
        /// <param name="wd">current project directory</param>
        /// <param name="registry">Registry URL if not using default</param>
        public NpmPackageManager(NpmFactory factory, string wd, string registry)
        {
            this.ApiClient = new NpmApi(factory, wd, registry);
        }

        /// <summary>
        /// Gets or sets the NpmApi client to use for invoking NPM and getting results
        /// </summary>
        public INpmApi ApiClient { get; set; }

        /// <summary>
        /// Get set of dependencies for project that are not installed or not up to date
        /// </summary>
        /// <param name="package">Packege to be checked</param>
        /// <returns>enumerable INpmInstalledPackage set</returns>
        public IEnumerable<INpmInstalledPackage> FindDependenciesToBeInstalled(INpmInstalledPackage package)
        {
            return null;
        }

        /// <summary>
        /// Find remote packages matching the given names
        /// </summary>
        /// <param name="packageIds">set of names</param>
        /// <returns>enumerable INpmRemotePackage set</returns>
        public IEnumerable<INpmRemotePackage> FindPackages(IEnumerable<string> packageIds)
        {
            return null;
        }

        /// <summary>
        /// Get installed packages within current project
        /// </summary>
        /// <returns>enumerable INpmInstalledPackage set</returns>
        public IEnumerable<INpmInstalledPackage> GetInstalledPackages()
        {
            return null;
        }

        /// <summary>
        /// Get list of available updates for installed packages
        /// </summary>
        /// <returns>enumerable INpmPackageDependency set</returns>
        public IEnumerable<INpmPackageDependency> GetPackagesWithUpdates()
        {
            return null;
        }

        /// <summary>
        /// Find all remote packages
        /// </summary>
        /// <returns>enumerable INpmSearchResultPackage set</returns>
        public IEnumerable<INpmSearchResultPackage> GetRemotePackages()
        {
            return null;
        }

        /// <summary>
        /// Install sepcified package
        /// </summary>
        /// <param name="package">name and optional version to be installed</param>
        /// <returns>enumerable INpmPackage set of all installed packages</returns>
        public IEnumerable<INpmPackage> InstallPackage(INpmPackage package)
        {
            return null;
        }

        /// <summary>
        /// Test if the package is installed in current project
        /// </summary>
        /// <param name="package">name and optional version to test</param>
        /// <returns>true or false</returns>
        public bool IsPackageInstalled(INpmPackage package)
        {
            return false;
        }

        /// <summary>
        /// Find all remote packages matching search terms
        /// </summary>
        /// <param name="searchTerms">set of terms</param>
        /// <returns>enumerable INpmSearchResultPackage set</returns>
        public IEnumerable<INpmSearchResultPackage> SearchRemotePackages(string searchTerms)
        {
            return this.ApiClient.Search(searchTerms);
        }

        /// <summary>
        /// Uninstall specified package from current project
        /// </summary>
        /// <param name="package">name of package</param>
        /// <returns>enumerable string set of packages removed</returns>
        public IEnumerable<string> UninstallPackage(INpmPackage package)
        {
            return null;
        }

        /// <summary>
        /// Update specified package in current directory
        /// </summary>
        /// <param name="package">name and optional version</param>
        /// <returns>enumerable string set of packages updated</returns>
        public IEnumerable<string> UpdatePackage(INpmPackage package)
        {
            return null;
        }
    }
}
