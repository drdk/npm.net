// -----------------------------------------------------------------------
// <copyright file="INpmSerialize.cs" company="Microsoft">
// Interface for npm package manager serialization routines
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// interface for serialization from npm to objects
    /// </summary>
    public interface INpmSerialize
    {
        /// <summary>
        /// converts npm list output to NpmInstalledPackage enumeration
        /// </summary>
        /// <param name="listJson">text output</param>
        /// <returns>enumerable NpmInstalledPackage properties</returns>
        IEnumerable<INpmInstalledPackage> FromListInstalled(string listJson);

        /// <summary>
        /// parse npm list output for matching NpmInstalledPackage
        /// </summary>
        /// <param name="listJson">text output</param>
        /// <param name="package">Installed package with name to match</param>
        /// <returns>NpmInstalledPackage properties or null</returns>
        INpmInstalledPackage FromListMatchInstalled(string listJson, INpmPackage package);

        /// <summary>
        /// convert npm view output to INpmRemotePackage
        /// </summary>
        /// <param name="viewJson">text output</param>
        /// <returns>INpmRemotePackage with property values</returns>
        INpmRemotePackage FromView(string viewJson);

        /// <summary>
        /// convert npm outdated output to NpmPackageDependency enumeration
        /// </summary>
        /// <param name="outdated">text output</param>
        /// <returns>enumerable INpmPackageDependency properties</returns>
        IEnumerable<INpmPackageDependency> FromOutdatedDependency(string outdated);

        /// <summary>
        /// convert npm search output to INpmSearchResultPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumerable INpmSearchResultPackage properties</returns>
        IEnumerable<INpmSearchResultPackage> FromSearchResult(string output);

        /// <summary>
        /// convert npm install output to INpmPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumerable INpmPackage properties</returns>
        IEnumerable<INpmInstalledPackage> FromInstall(string output);
    }
}
