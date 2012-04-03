using System;
using System.Collections.Generic;
using System.Text;

namespace Webmatrix_Npm
{
    internal interface INpmPackageManager
    {
        IEnumerable<INpmInstalledPackage> FindDependenciesToBeInstalled(INpmPackage package);

        IEnumerable<INpmRemotePackage> FindPackages(IEnumerable<string> packageIds);

        IEnumerable<INpmInstalledPackage> GetInstalledPackages();

        IEnumerable<INpmPackageDependency> GetPackagesWithUpdates();

        IEnumerable<INpmSearchResultPackage> GetRemotePackages();

        IEnumerable<INpmPackage> InstallPackage(INpmPackage package);

        bool IsPackageInstalled(INpmPackage package);

        IEnumerable<INpmSearchResultPackage> SearchRemotePackages(string searchTerms);

        IEnumerable<string> UninstallPackage(INpmPackage package);

        IEnumerable<string> UpdatePackage(INpmPackage package);
    }
}
