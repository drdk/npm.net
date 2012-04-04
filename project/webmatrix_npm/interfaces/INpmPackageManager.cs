namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// High level interface to manage NPM installation
    /// </summary>
    internal interface INpmPackageManager
    {
        /// <summary>
        /// Get set of dependencies for project that are not installed or not up to date
        /// </summary>
        /// <param name="package">Packege to be checked</param>
        /// <returns>enumerable INpmInstalledPackage set</returns>
        IEnumerable<INpmInstalledPackage> FindDependenciesToBeInstalled(INpmPackage package);

        /// <summary>
        /// Find remote packages matching the given names
        /// </summary>
        /// <param name="packageIds">set of names</param>
        /// <returns>enumerable INpmRemotePackage set</returns>
        IEnumerable<INpmRemotePackage> FindPackages(IEnumerable<string> packageIds);

        /// <summary>
        /// Get installed packages within current project
        /// </summary>
        /// <returns>enumerable INpmInstalledPackage set</returns>
        IEnumerable<INpmInstalledPackage> GetInstalledPackages();

        /// <summary>
        /// Get list of available updates for installed packages
        /// </summary>
        /// <returns>enumerable INpmPackageDependency set</returns>
        IEnumerable<INpmPackageDependency> GetPackagesWithUpdates();

        /// <summary>
        /// Find all remote packages
        /// </summary>
        /// <returns>enumerable INpmSearchResultPackage set</returns>
        IEnumerable<INpmSearchResultPackage> GetRemotePackages();

        /// <summary>
        /// Install sepcified package
        /// </summary>
        /// <param name="package">name and optional version to be installed</param>
        /// <returns>enumerable INpmPackage set of all installed packages</returns>
        IEnumerable<INpmPackage> InstallPackage(INpmPackage package);

        /// <summary>
        /// Test if the package is installed in current project
        /// </summary>
        /// <param name="package">name and optional version to test</param>
        /// <returns>true or false</returns>
        bool IsPackageInstalled(INpmPackage package);

        /// <summary>
        /// Find all remote packages matching search terms
        /// </summary>
        /// <param name="searchTerms">space separated search terms</param>
        /// <returns>enumerable INpmSearchResultPackage set</returns>
        IEnumerable<INpmSearchResultPackage> SearchRemotePackages(string searchTerms);

        /// <summary>
        /// Uninstall specified package from current project
        /// </summary>
        /// <param name="package">name of package</param>
        /// <returns>enumerable string set of packages removed</returns>
        IEnumerable<string> UninstallPackage(INpmPackage package);

        /// <summary>
        /// Update specified package in current directory
        /// </summary>
        /// <param name="package">name and optional version</param>
        /// <returns>enumerable string set of packages updated</returns>
        IEnumerable<string> UpdatePackage(INpmPackage package);
    }
}
