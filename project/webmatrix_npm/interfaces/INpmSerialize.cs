namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// interface for serialization from npm to objects
    /// </summary>
    internal interface INpmSerialize
    {
        /// <summary>
        /// converts npm list output to NpmInstalledPackage enumeration
        /// </summary>
        /// <param name="jsonlist">text output</param>
        /// <returns>enumarable NpmInstalledPackage properties</returns>
        INpmInstalledPackage FromListInstalled(string jsonlist);

        /// <summary>
        /// convert npm view output to INpmRemotePackage
        /// </summary>
        /// <param name="jsonview">text output</param>
        /// <returns>INpmRemotePackage with property values</returns>
        INpmRemotePackage FromView(string jsonview);

        /// <summary>
        /// convert missing info from npm list output to NpmInstalledPackage enumeration
        /// </summary>
        /// <param name="jsonlist">text output</param>
        /// <returns>enumarable NpmInstalledPackage properties</returns>
        IEnumerable<INpmInstalledPackage> FromListMissing(string jsonlist);

        /// <summary>
        /// convert outdated info from npm list output to NpmInstalledPackage enumeration
        /// </summary>
        /// <param name="jsonlist">text output</param>
        /// <returns>enumarable NpmInstalledPackage properties</returns>
        IEnumerable<INpmInstalledPackage> FromListOutdated(string jsonlist);

        /// <summary>
        /// convert npm outdated output to NpmPackageDependency enumeration
        /// </summary>
        /// <param name="outdated">text output</param>
        /// <returns>enumarable INpmPackageDependency properties</returns>
        IEnumerable<INpmPackageDependency> FromOutdatedDependency(string outdated);

        /// <summary>
        /// convert npm search output to INpmSearchResultPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumarable INpmSearchResultPackage properties</returns>
        IEnumerable<INpmSearchResultPackage> FromSearchResult(string output);

        /// <summary>
        /// convert npm install output to INpmPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumarable INpmPackage properties</returns>
        IEnumerable<INpmPackage> FromInstall(string output);
    }
}
