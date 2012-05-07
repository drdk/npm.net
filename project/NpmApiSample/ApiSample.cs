// -----------------------------------------------------------------------
// <copyright file="ApiSample.cs" company="Microsoft">
// Sample usage of NpmApi
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
    /// A class that uses NpmApi
    /// </summary>
    public static class ApiSample
    {
        /// <summary>
        /// Exercise the NpmApi class
        /// </summary>
        /// <param name="wd">working directory path</param>
        /// <param name="installPath">node installation path</param>
        /// <param name="module">module name to use</param>
        /// <returns>true or false</returns>
        public static bool RunSample(string wd, string installPath, string module)
        {
            try
            {
                string uninstalledName = null;
                NpmApi npm = new NpmApi(wd);
                if (npm == null)
                {
                    Console.WriteLine("Failed to create NpmApi");
                    return false;
                }

                npm.NpmClient.InstallPath = installPath;

                string version = npm.GetInstalledVersion();
                INpmSearchResultPackage found = null;
                IEnumerable<INpmSearchResultPackage> searchResults = npm.Search(module);
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
                    Console.WriteLine("Search failed to find '{0}'", module);
                    return false;
                }

                // install module as a dependency
                IEnumerable<INpmPackage> installed = npm.Install(found);
                if (installed == null || installed.Count() == 0)
                {
                    Console.WriteLine("Install failed for {0}", found.Name);
                    return false;
                }

                // list packages at parent
                IEnumerable<INpmInstalledPackage> installedPkg = npm.List();
                if (installedPkg == null)
                {
                    Console.WriteLine("List failed for {0}", found.Name);
                    return false;
                }

                // there should be at least 1 item since we installed one
                if (installedPkg.Count() == 0)
                {
                    Console.WriteLine("There are no packages listed");
                    return false;
                }

                // switch to dependency directory to uninstall a child
                npm.SetDependencyDirectory(found.Name);

                // remove a dependency
                IEnumerable<INpmInstalledPackage> installedChildren = npm.ListChildren();
                foreach (INpmInstalledPackage package in installedChildren)
                {
                    if (!npm.Uninstall(package.Name))
                    {
                        Console.WriteLine("Failed to uninstall dependency {0} of {1}", package.Name, found.Name);
                        return false;
                    }
                    else
                    {
                        uninstalledName = package.Name;
                    }

                    break;
                }

                // revert directory
                npm.SetDependencyDirectory(null);

                // check that it is reported as missing
                bool matchMissing = false;
                IEnumerable<INpmPackageDependency> outdated = npm.Outdated();
                if (outdated != null && outdated.Count() > 0)
                {
                    foreach (INpmPackageDependency outofdate in outdated)
                    {
                        if (outofdate.Name == uninstalledName &&
                            string.IsNullOrWhiteSpace(outofdate.Version))
                        {
                            matchMissing = true;
                            break;
                        }
                    }
                }

                if (!matchMissing)
                {
                    Console.WriteLine("Expected at least one outdated entry after uninstall of {0}", uninstalledName);
                    return false;
                }

                // now call update and check if it is fixed
                installedPkg = npm.Update(uninstalledName);
                if (installedPkg == null)
                {
                    Console.WriteLine("Update failed for {0}", uninstalledName);
                    return false;
                }

                // check that specified package is updated
                matchMissing = false;
                foreach (INpmInstalledPackage package in installedPkg)
                {
                    if (package.Name == uninstalledName)
                    {
                        matchMissing = true;
                        break;
                    }
                }

                if (!matchMissing)
                {
                    Console.WriteLine("Package not reported as updated for {0}", uninstalledName);
                    return false;
                }

                outdated = npm.Outdated();
                if (outdated != null && outdated.Count() > 0)
                {
                    Console.WriteLine("Expected no outdated entry after update of {0}", module);
                    return false;
                }

                Console.WriteLine("Success! {0} is installed.", module);
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
