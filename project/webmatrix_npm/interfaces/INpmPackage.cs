using System;
using System.Collections.Generic;
using System.Text;

namespace Webmatrix_Npm
{
    internal interface INpmPackage
    {
        string Name { get; set; }

        string Version { get; set; }
    }

    internal interface INpmPackageDependency : INpmPackage
    {
        string VersionRange { get; }
    }

    internal interface INpmInstalledPackage : INpmPackage
    {
        IEnumerable<INpmInstalledPackage> InstalledDependencies { get; }

        IEnumerable<INpmPackageDependency> MissingDependencies { get; }

        IEnumerable<INpmPackageDependency> OutdatedDependencies { get; }
    }

    internal interface INpmRemotePackage : INpmPackage
    {
        string Description { get; }

        IEnumerable<string> Versions { get; }

        IEnumerable<string> Maintainers { get; }

        IEnumerable<string> Contributors { get; }

        string HomepageUrl { get; }

        string Author { get; }

        IEnumerable<string> Files { get; }

        IEnumerable<INpmPackageDependency> Dependencies { get; }

        IEnumerable<INpmPackageDependency> DevDependencies { get; }

        IEnumerable<INpmPackageDependency> OptionalDependencies { get; }

        INpmReference License { get; }

        INpmReference Repository { get; }
    }

    internal interface INpmSearchResultPackage : INpmPackage
    {
        string Description { get; }

        string Author { get; }

        string[] Keywords { get; }

        DateTime Date { get; }
    }
}
