// -----------------------------------------------------------------------
// <copyright file="INpmPackageManager.cs" company="Microsoft Open Technologies, Inc.">
// Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0.
//
// THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT
// LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR
// A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache License, Version 2.0 for specific language governing
// permissions and limitations under the License.
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// High level interface to manage NPM installation
    /// </summary>
    public interface INpmPackageManager
    {
        /// <summary>
        /// Get set of dependencies for project that are not installed or not up to date
        /// </summary>
        /// <param name="package">Packege to be checked</param>
        /// <returns>enumerable INpmInstalledPackage set</returns>
        IEnumerable<INpmPackageDependency> FindDependenciesToBeInstalled(INpmPackage package);

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
        void InstallPackage(INpmPackage package);

        /// <summary>
        /// Test if the package is installed in current project
        /// </summary>
        /// <param name="package">name and optional version to test</param>
        /// <returns>INpmInstalledPackage or null</returns>
        INpmInstalledPackage IsPackageInstalled(INpmPackage package);

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
        void UninstallPackage(INpmPackage package);

        /// <summary>
        /// Update specified package in current directory
        /// </summary>
        /// <param name="package">name and optional version</param>
        void UpdatePackage(INpmPackage package);
    }
}
