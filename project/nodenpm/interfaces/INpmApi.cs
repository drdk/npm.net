// -----------------------------------------------------------------------
// <copyright file="INpmApi.cs" company="Microsoft">
// Interface for npm package manager low level API
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Low level NPM API wrapper
    /// </summary>
    public interface INpmApi
    {
        /// <summary>
        /// Gets the INpmClient interface being used
        /// </summary>
        INpmClient NpmClient
        {
            get;
        }

        /// <summary>
        /// Get npm version. Wraps 'npm --version'
        /// </summary>
        /// <returns>version string</returns>
        string GetInstalledVersion();

        /// <summary>
        /// Set working directory for dependency.
        /// </summary>
        /// <param name="dependency">Dependency path</param>
        /// <remarks>Use '/' for multiple level dependency</remarks>
        void SetDependencyDirectory(string dependency);

        /// <summary>
        /// Change the working directory
        /// </summary>
        /// <param name="path">Full path</param>
        void SetWorkingDirectory(string path);

        /// <summary>
        /// Get installed modules in project. Wraps 'npm list'
        /// </summary>
        /// <returns>enumerable NpmInstalledPackage properties</returns>
        IEnumerable<INpmInstalledPackage> List();

        /// <summary>
        /// Get installed child modules in project. Wraps 'npm list'
        /// </summary>
        /// <returns>enumerable NpmInstalledPackage properties</returns>
        IEnumerable<INpmInstalledPackage> ListChildren();

        /// <summary>
        /// Get properties of package in repository. Wraps 'npm view name'
        /// </summary>
        /// <param name="name">package name</param>
        /// <returns>NpmRemotePackage properties</returns>
        INpmRemotePackage View(string name);

        /// <summary>
        /// Search for npm packages in repository. Wraps 'npm search term'
        /// </summary>
        /// <param name="searchTerms">words to use in search</param>
        /// <returns>enumerable set of matching packages</returns>
        IEnumerable<INpmSearchResultPackage> Search(string searchTerms);

        /// <summary>
        /// Install a npm package. Wraps 'npm install name'
        /// </summary>
        /// <param name="package">name and version to install</param>
        /// <returns>enumerable list of packages</returns>
        IEnumerable<INpmInstalledPackage> Install(INpmPackage package);

        /// <summary>
        /// Get outdated or missing dependencies. Wraps 'npm outdated'
        /// </summary>
        /// <returns>enumerable set of packages needing updates</returns>
        IEnumerable<INpmPackageDependency> Outdated();

        /// <summary>
        /// Check if dependency is outdated. Wraps 'npm outdated name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>enumerable set of packages needing updates</returns>
        IEnumerable<INpmPackageDependency> Outdated(string name);

        /// <summary>
        /// Check if dependencies are outdated. Wraps 'npm outdated name name2'
        /// </summary>
        /// <param name="packages">set of packages to test</param>
        /// <returns>enumerable set of packages needing updates</returns>
        IEnumerable<INpmPackageDependency> Outdated(IEnumerable<INpmPackage> packages);

        /// <summary>
        /// Update named package. Wraps 'npm update name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        IEnumerable<INpmInstalledPackage> Update(string name);

        /// <summary>
        /// Uninstall named package. Wraps 'npm uninstall name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        bool Uninstall(string name);

        /// <summary>
        /// Check if package is installed. Wraps 'npm list' and looks for match
        /// </summary>
        /// <param name="package">name and version to install</param>
        /// <returns>NpmInstalledPackage or null</returns>
        INpmInstalledPackage TestInstalled(INpmPackage package);
    }
}
