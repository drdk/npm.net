// -----------------------------------------------------------------------
// <copyright file="NpmPackageManager.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.Security;
    using System.Text;

    /// <summary>
    /// High level class to manage NPM installation
    /// </summary>
    public class NpmPackageManager : INpmPackageManager
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
        /// <param name="registry">registry URI if not using default</param>
        public NpmPackageManager(string wd, Uri registry)
        {
            this.ApiClient = new NpmApi(wd, registry);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmPackageManager" /> class.
        /// </summary>
        /// <param name="factory">NpmFactory class</param>
        /// <param name="wd">current project directory</param>
        /// <param name="registry">Registry URI if not using default</param>
        public NpmPackageManager(NpmFactory factory, string wd, Uri registry)
        {
            this.ApiClient = new NpmApi(factory, wd, registry);
        }

        /// <summary>
        /// Gets or sets the NpmApi client to use for invoking NPM and getting results
        /// </summary>
        public INpmApi ApiClient
        { 
            get;
            set;
        }

        /// <summary>
        /// Gets the NpmClient interface to use for invoking NPM and getting results
        /// </summary>
        public INpmClient NpmClient
        {
            get
            {
                return this.ApiClient.NpmClient;
            }
        }

        /// <summary>
        /// Get set of dependencies for project that are not installed or not up to date
        /// </summary>
        /// <param name="package">Package to be checked</param>
        /// <returns>enumerable INpmInstalledPackage set</returns>
        public IEnumerable<INpmPackageDependency> FindDependenciesToBeInstalled(INpmPackage package)
        {
            if (package == null)
            {
                // check everything
                return this.ApiClient.Outdated();
            }
            else
            {
                // check specific package, and if it has dependencies
                // then change to package directory and check children
                if (string.IsNullOrWhiteSpace(package.Name))
                {
                    throw new ArgumentException("package.name is required");
                }

                IEnumerable<INpmPackageDependency> outdatedList = this.ApiClient.Outdated(package.Name);

                IEnumerable<INpmInstalledPackage> beforePackages = this.ApiClient.List();

                IEnumerable<INpmInstalledPackage> matchedList = beforePackages.Where(r => r.Name == package.Name).AsEnumerable();
                if (matchedList != null && matchedList.Count() > 0)
                {
                    INpmInstalledPackage matched = matchedList.First();
                    if (matched != null && matched.HasDependencies)
                    {
                        if (!string.IsNullOrWhiteSpace(matched.Name))
                        {
                            this.ApiClient.SetDependencyDirectory(matched.Name);
                        }

                        IEnumerable<INpmPackageDependency> outdatedChildren = this.ApiClient.Outdated();
                        if (outdatedChildren != null && outdatedChildren.Count() > 0)
                        {
                            outdatedList = outdatedList.Concat(outdatedChildren);
                        }

                        this.ApiClient.SetDependencyDirectory(null);
                    }
                }

                return outdatedList;
            }
        }

        /// <summary>
        /// Find remote packages matching the given names
        /// </summary>
        /// <param name="packageIds">set of names</param>
        /// <returns>enumerable INpmRemotePackage set</returns>
        public IEnumerable<INpmRemotePackage> FindPackages(IEnumerable<string> packageIds)
        {
            if (packageIds == null)
            {
                throw new ArgumentNullException("packageIds");
            }

            List<INpmRemotePackage> packages = new List<INpmRemotePackage>();
            foreach (string name in packageIds)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    INpmRemotePackage remote = this.ApiClient.View(name);
                    if (remote != null)
                    {
                        packages.Add(remote);
                    }
                }
            }

            return packages;
        }

        /// <summary>
        /// Get installed packages within current project
        /// </summary>
        /// <returns>enumerable INpmInstalledPackage set</returns>
        public IEnumerable<INpmInstalledPackage> GetInstalledPackages()
        {
            return this.ApiClient.ListChildren();
        }

        /// <summary>
        /// Get list of available updates for installed packages
        /// </summary>
        /// <returns>enumerable INpmPackageDependency set</returns>
        public IEnumerable<INpmPackageDependency> GetPackagesWithUpdates()
        {
            IEnumerable<INpmInstalledPackage> children = this.ApiClient.ListChildren();
            if (children != null && children.Count() > 0)
            {
                return this.ApiClient.Outdated(children);
            }

            return new List<INpmPackageDependency>();
        }

        /// <summary>
        /// Find all remote packages
        /// </summary>
        /// <returns>enumerable INpmSearchResultPackage set</returns>
        public IEnumerable<INpmSearchResultPackage> GetRemotePackages()
        {
            return this.ApiClient.Search(null);
        }

        /// <summary>
        /// Install sepcified package
        /// </summary>
        /// <param name="package">name and optional version to be installed</param>
        public void InstallPackage(INpmPackage package)
        {
            this.ApiClient.Install(package);
        }

        /// <summary>
        /// Test if the package is installed in current project
        /// </summary>
        /// <param name="package">name and optional version to test</param>
        /// <returns>INpmInstalledPackage or null</returns>
        public INpmInstalledPackage IsPackageInstalled(INpmPackage package)
        {
            IEnumerable<INpmInstalledPackage> children = this.ApiClient.ListChildren();
            if (children != null && children.Count() > 0)
            {
                foreach (INpmInstalledPackage child in children)
                {
                    if (child.Name == package.Name && !child.IsMissing)
                    {
                        // if version specified, match version as well as name
                        if (!string.IsNullOrWhiteSpace(package.Version))
                        {
                            if (child.Version == package.Version)
                            {
                                return child;
                            }
                            else
                            {
                                return null;
                            }
                        }

                        return child;
                    }
                }
            }

            return null;
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
        public void UninstallPackage(INpmPackage package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.ApiClient.Uninstall(package.Name);
        }

        /// <summary>
        /// Update specified package in current directory
        /// </summary>
        /// <param name="package">name and optional version</param>
        public void UpdatePackage(INpmPackage package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            if (string.IsNullOrWhiteSpace(package.Name))
            {
                throw new ArgumentException("package.name is required");
            }

            // first update this package by name
            IEnumerable<INpmInstalledPackage> updatedList = this.ApiClient.Update(package.Name);

            IEnumerable<INpmInstalledPackage> beforePackages = this.ApiClient.ListChildren();

            // find package, and build path if needed
            IEnumerable<INpmInstalledPackage> matchedList = beforePackages.Where(r => r.Name == package.Name).AsEnumerable();
            INpmInstalledPackage matched = null;
            if (matchedList != null && matchedList.Count() > 0)
            {
                matched = matchedList.First();
            }

            // update the package descendents
            if (matched != null && matched.HasDependencies)
            {
                if (!string.IsNullOrWhiteSpace(matched.Name))
                {
                    this.ApiClient.SetDependencyDirectory(matched.Name);
                }

                IEnumerable<INpmInstalledPackage> updatedChildren = this.ApiClient.Update(null);

                this.ApiClient.SetDependencyDirectory(null);
            }
        }
    }
}
