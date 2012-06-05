// -----------------------------------------------------------------------
// <copyright file="ManualPmSample.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.Linq;
    using System.Text;
    using NodeNpm;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ManualPmSample
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
            NpmPackageManager npm = new NpmPackageManager(wd);
            if (npm == null)
            {
                Console.WriteLine("Failed to create NpmPackageManager");
                return false;
            }

            npm.NpmClient.InstallPath = installPath;

            bool quit = false;
            ShowUsage();
            while (!quit)
            {
                try
                {
                    Console.Write(">: ");
                    string cmd = Console.ReadLine();
                    char[] seps = new char[] { ' ' };
                    string[] tokens = cmd.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                    int numTokens = tokens.Length;
                    if (numTokens > 0)
                    {
                        switch (tokens[0].ToLower())
                        {
                            case "q":
                                quit = true;
                                break;

                            case "list":
                                DoList(npm);
                                break;

                            case "install":
                                if (numTokens > 1)
                                {
                                    DoInstall(npm, tokens[1]);
                                }
                                else
                                {
                                    Console.WriteLine("install needs name of package");
                                }

                                break;

                            case "uninstall":
                                if (numTokens > 1)
                                {
                                    DoUninstall(npm, tokens[1]);
                                }
                                else
                                {
                                    Console.WriteLine("uninstall needs name of package");
                                }

                                break;

                            case "find":
                                if (numTokens > 1)
                                {
                                    DoFind(npm, tokens[1]);
                                }
                                else
                                {
                                    Console.WriteLine("find needs search term");
                                }

                                break;

                            case "depends":
                                DoDepends(npm);
                                break;

                            case "update":
                                if (numTokens > 1)
                                {
                                    DoUpdate(npm, tokens[1]);
                                }
                                else
                                {
                                    Console.WriteLine("update needs name of package");
                                }

                                break;

                            default:
                                ShowUsage();
                                break;
                        }
                    }
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
                }
            }

            return true;
        }

        /// <summary>
        /// Show allowed commands
        /// </summary>
        private static void ShowUsage()
        {
            Console.WriteLine("enter one of the following:");
            Console.WriteLine("q");
            Console.WriteLine("list");
            Console.WriteLine("install <name>");
            Console.WriteLine("uninstall <name>");
            Console.WriteLine("find <term>");
            Console.WriteLine("depends");
            Console.WriteLine("update <name>");
        }

        /// <summary>
        /// Execute GetInstalledPackages
        /// </summary>
        /// <param name="npm">NpmPackageManager instance</param>
        private static void DoList(NpmPackageManager npm)
        {
            IEnumerable<INpmInstalledPackage> installedPkg = npm.GetInstalledPackages();
            if (installedPkg == null || installedPkg.Count() == 0)
            {
                Console.WriteLine("GetInstalledPackages returned nothing");
                return;
            }

            foreach (INpmInstalledPackage pkg in installedPkg)
            {
                Console.WriteLine(pkg.Name);
            }
        }

        /// <summary>
        /// Execute InstallPackage
        /// </summary>
        /// <param name="npm">NpmPackageManager instance</param>
        /// <param name="name">name of package</param>
        private static void DoInstall(NpmPackageManager npm, string name)
        {
            NpmPackage pkg = new NpmPackage(name, null);
            npm.InstallPackage(pkg);
        }

        /// <summary>
        /// Execute UninstallPackage
        /// </summary>
        /// <param name="npm">NpmPackageManager instance</param>
        /// <param name="name">name of package</param>
        private static void DoUninstall(NpmPackageManager npm, string name)
        {
            NpmPackage pkg = new NpmPackage(name, null);
            npm.UninstallPackage(pkg);
        }

        /// <summary>
        /// Execute SearchRemotePackages
        /// </summary>
        /// <param name="npm">NpmPackageManager instance</param>
        /// <param name="term">search term</param>
        private static void DoFind(NpmPackageManager npm, string term)
        {
            IEnumerable<INpmSearchResultPackage> searchResults = npm.SearchRemotePackages(term);
            if (searchResults != null)
            {
                foreach (INpmSearchResultPackage result in searchResults)
                {
                    Console.WriteLine(result.Name);
                }
            }
        }

        /// <summary>
        /// Execute GetPackagesWithUpdates
        /// </summary>
        /// <param name="npm">NpmPackageManager instance</param>
        private static void DoDepends(NpmPackageManager npm)
        {
            IEnumerable<INpmPackageDependency> pkgs;
            pkgs = npm.GetPackagesWithUpdates();
            foreach (INpmPackageDependency pkg in pkgs)
            {
                Console.WriteLine(pkg.Name);
            }
        }

        /// <summary>
        /// Execute UpdatePackage
        /// </summary>
        /// <param name="npm">NpmPackageManager instance</param>
        /// <param name="name">name of package</param>
        private static void DoUpdate(NpmPackageManager npm, string name)
        {
            NpmPackage pkg = new NpmPackage(name, null);
            npm.UpdatePackage(pkg);
        }
    }
}
