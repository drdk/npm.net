// -----------------------------------------------------------------------
// <copyright file="NpmPackageManager.cs" company="Microsoft">
// Class for some npm package manager high level API
// </copyright>
// -----------------------------------------------------------------------

namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// High level class to manage NPM installation
    /// </summary>
    public class NpmPackageManager
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
        /// <param name="registry">registry URL if not using default</param>
        public NpmPackageManager(string wd, string registry)
        {
            this.ApiClient = new NpmApi(wd, registry);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmPackageManager" /> class.
        /// </summary>
        /// <param name="factory">NpmFactory class</param>
        /// <param name="wd">current project directory</param>
        /// <param name="registry">Registry URL if not using default</param>
        public NpmPackageManager(NpmFactory factory, string wd, string registry)
        {
            this.ApiClient = new NpmApi(factory, wd, registry);
        }

        /// <summary>
        /// Gets or sets the NpmApi client to use for invoking NPM and getting results
        /// </summary>
        public INpmApi ApiClient { get; set; }

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
            return this.ApiClient.List();
        }

        /// <summary>
        /// Get list of available updates for installed packages
        /// </summary>
        /// <returns>enumerable INpmPackageDependency set</returns>
        public IEnumerable<INpmPackageDependency> GetPackagesWithUpdates()
        {
            return this.ApiClient.Outdated();
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
        /// <returns>enumerable INpmPackage set of all installed packages</returns>
        public IEnumerable<INpmInstalledPackage> InstallPackage(INpmPackage package)
        {
            return this.ApiClient.Install(package);
        }

        /// <summary>
        /// Test if the package is installed in current project
        /// </summary>
        /// <param name="package">name and optional version to test</param>
        /// <returns>INpmInstalledPackage or null</returns>
        public INpmInstalledPackage IsPackageInstalled(INpmPackage package)
        {
            return this.ApiClient.TestInstalled(package);
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
        /// <returns>enumerable string set of packages removed</returns>
        public IEnumerable<string> UninstallPackage(INpmPackage package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            IEnumerable<INpmInstalledPackage> beforePackages = this.ApiClient.List();

            // find package, and build path if needed
            IEnumerable<INpmInstalledPackage> matchedList = beforePackages.Where(r => r.Name == package.Name).AsEnumerable();
            if (matchedList == null || matchedList.Count() == 0)
            {
                return null;
            }

            INpmInstalledPackage matched = matchedList.First();

            if (!string.IsNullOrWhiteSpace(matched.DependentPath))
            {
                this.ApiClient.SetDependencyDirectory(matched.DependentPath);
            }

            bool uninstalled = this.ApiClient.Uninstall(package.Name);

            this.ApiClient.SetDependencyDirectory(null);

            if (uninstalled)
            {
                IEnumerable<INpmInstalledPackage> afterPackages = this.ApiClient.List();

                if (beforePackages != null && afterPackages != null)
                {
                    List<string> uninstalledList = new List<string>();
                    foreach (INpmInstalledPackage before in beforePackages)
                    {
                        if (!afterPackages.Where(r => r.Name == before.Name && !r.IsMissing).Any())
                        {
                            uninstalledList.Add(before.Name);
                        }
                    }

                    return uninstalledList;
                }
            }

            return null;
        }

        /// <summary>
        /// Update specified package in current directory
        /// </summary>
        /// <param name="package">name and optional version</param>
        /// <returns>enumerable string set of packages updated</returns>
        public IEnumerable<string> UpdatePackage(INpmPackage package)
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

            IEnumerable<INpmInstalledPackage> beforePackages = this.ApiClient.List();

            // find package, and build path if needed
            IEnumerable<INpmInstalledPackage> matchedList = beforePackages.Where(r => r.Name == package.Name).AsEnumerable();
            if (matchedList == null || matchedList.Count() == 0)
            {
                return null;
            }

            INpmInstalledPackage matched = matchedList.First();
            if (matched != null && matched.HasDependencies)
            {
                if (!string.IsNullOrWhiteSpace(matched.Name))
                {
                    this.ApiClient.SetDependencyDirectory(matched.Name);
                }

                IEnumerable<INpmInstalledPackage> updatedChildren = this.ApiClient.Update(null);
                if (updatedChildren != null && updatedChildren.Count() > 0)
                {
                    updatedList = updatedList.Concat(updatedChildren);
                }

                this.ApiClient.SetDependencyDirectory(null);
            }

            if (updatedList != null)
            {
                List<string> names = new List<string>();
                foreach (INpmInstalledPackage updated in updatedList)
                {
                    if (!string.IsNullOrWhiteSpace(updated.Name))
                    {
                        names.Add(updated.Name);
                    }
                }

                return names;
            }

            return null;
        }
    }
}
