﻿// -----------------------------------------------------------------------
// <copyright file="PackageManagerSample.cs" company="Microsoft Open Technologies, Inc.">
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

namespace NpmApiSample
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using NodeNpm;

    /// <summary>
    /// A class that uses NpmPackageManager
    /// </summary>
    public static class PackageManagerSample
    {
        /// <summary>
        /// Exercise the NpmPackageManager class
        /// </summary>
        /// <param name="wd">working directory path</param>
        /// <param name="installPath">node installation path</param>
        /// <param name="module">module name to use</param>
        /// <returns>true or false</returns>
        public static bool RunSample(string wd, string installPath, string module)
        {
            try
            {
                NpmPackageManager npm = new NpmPackageManager(wd);
                if (npm == null)
                {
                    Console.WriteLine("Failed to create NpmPackageManager");
                    return false;
                }

                npm.NpmClient.InstallPath = installPath;
                INpmSearchResultPackage found = null;
                IEnumerable<INpmSearchResultPackage> searchResults = npm.SearchRemotePackages(module);
                if (searchResults != null)
                {
                    foreach (INpmSearchResultPackage result in searchResults)
                    {
                        if (result.Name == module)
                        {
                            found = result;
                            break;
                        }
                    }
                }

                if (found == null)
                {
                    Console.WriteLine("SearchRemotePackages failed to find '{0}'", module);
                    return false;
                }

                // install module as a dependency
                npm.InstallPackage(found);

                // list packages at parent
                IEnumerable<INpmInstalledPackage> installedPkg = npm.GetInstalledPackages();
                if (installedPkg == null)
                {
                    Console.WriteLine("GetInstalledPackages failed for {0}", found.Name);
                    return false;
                }

                // there should be at least 1 item since we installed one
                if (installedPkg.Count() == 0)
                {
                    Console.WriteLine("There are no packages listed");
                    return false;
                }

                // now call update
                npm.UpdatePackage(found);

                IEnumerable<INpmPackageDependency> outdated = npm.FindDependenciesToBeInstalled(found);
                if (outdated != null && outdated.Count() > 0)
                {
                    Console.WriteLine("Expected no outdated entry after update of {0}", module);
                    return false;
                }

                npm.UninstallPackage(installedPkg.First());

                Console.WriteLine("Success! {0} was installed and uninstalled.", module);
                return true;
            }
            catch (NpmException ex)
            {
                Console.WriteLine("Npm failed with exception.");
                Console.WriteLine("Message: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner message: " + ex.InnerException.Message);
                }
                else
                {
                    if (ex.NpmCode != null)
                    {
                        Console.WriteLine("Code: " + ex.NpmCode);
                    }

                    if (ex.NpmCommand != null)
                    {
                        Console.WriteLine("calling: " + ex.NpmCommand);
                    }
                }

                return false;
            }
        }
    }
}
