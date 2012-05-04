// -----------------------------------------------------------------------
// <copyright file="PackageManagerSample.cs" company="Microsoft">
// Sample usage of NpmPackageManager
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
                    Console.WriteLine("Failed to create NpmApi");
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

                IEnumerable<INpmPackageDependency>  outdated = npm.FindDependenciesToBeInstalled(found);
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
