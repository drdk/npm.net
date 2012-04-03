using System;
using System.Collections.Generic;
using System.Text;

namespace Webmatrix_Npm
{
    internal class NpmPackageManager
    {
        public IEnumerable<INpmInstalledPackage> FindDependenciesToBeInstalled(INpmInstalledPackage package)
        {
            return null;
        }
        public IEnumerable<INpmRemotePackage> FindPackages(IEnumerable<string> packageIds)
        {
            return null;
        }

        public IEnumerable<INpmInstalledPackage> GetInstalledPackages()
        {
            return null;
        }

        public IEnumerable<INpmPackageDependency> GetPackagesWithUpdates()
        {
            return null;
        }

        public IEnumerable<INpmSearchResultPackage> GetRemotePackages()
        {
            return null;
        }

        public IEnumerable<INpmPackage> InstallPackage(INpmPackage package)
        {
            return null;
        }

        public bool IsPackageInstalled(INpmPackage package)
        {
            return false;
        }

        public IEnumerable<INpmSearchResultPackage> SearchRemotePackages(string searchTerms)
        {
            return null;
        }
        public IEnumerable<string> UninstallPackage(INpmPackage package)
        {
            return null;
        }
        public IEnumerable<string> UpdatePackage(INpmPackage package)
        {
            return null;
        }

        internal NpmPackageManager(string cwd, string registry)
        {
            // get NPM version. Call factory class to get client and serializer
        }

        internal INpmClient Client { get; set; }

        private INpmSerialize Serializer { get; set; }

        internal string NpmVersion { get; private set; }


    }
}
