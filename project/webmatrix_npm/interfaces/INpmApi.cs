namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Low level NPM API wrapper
    /// </summary>
    internal interface INpmApi
    {
        /// <summary>
        /// Get npm version. Wraps 'npm --version'
        /// </summary>
        /// <returns>version string</returns>
        string GetInstalledVersion();

        /// <summary>
        /// Get installed modules n project. Wraps 'npm list'
        /// </summary>
        /// <returns>enumerable set of installed packages</returns>
        IEnumerable<INpmInstalledPackage> List();

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
        /// <returns>true or false</returns>
        bool Install(INpmPackage package);

        /// <summary>
        /// Get outdated or missing dependencies. Wraps 'npm outdated'
        /// </summary>
        /// <returns>enumerable set of packages needing updates</returns>
        IEnumerable<INpmPackage> Outdated();

        /// <summary>
        /// Check if dependency is outdated. Wraps 'npm outdated name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>npm package with newer version</returns>
        INpmPackage Outdated(string name);

        /// <summary>
        /// Update named package. Wraps 'npm update name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        bool Update(string name);

        /// <summary>
        /// Uninstall named package. Wraps 'npm uninstall name'
        /// </summary>
        /// <param name="name">name of package</param>
        /// <returns>true or false</returns>
        bool Uninstall(string name);
    }
}
