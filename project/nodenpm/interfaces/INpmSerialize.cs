// -----------------------------------------------------------------------
// <copyright file="INpmSerialize.cs" company="Microsoft Open Technologies, Inc.">
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
        /// converts npm list output to NpmInstalledPackage enumeration of immediate children
        /// </summary>
        /// <param name="listJson">text output</param>
        /// <returns>enumerable NpmInstalledPackage properties</returns>
        IEnumerable<INpmInstalledPackage> FromListInstalledChildren(string listJson);

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

        /// <summary>
        /// Creates an NpmException with properties from the error output
        /// </summary>
        /// <param name="output">The error stream output from npm</param>
        /// <returns>a new NpmException</returns>
        NpmException ExceptionFromError(string output);
    }
}
