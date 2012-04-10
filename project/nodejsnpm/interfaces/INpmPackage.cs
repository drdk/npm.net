namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// INpmPackage has name and optional version
    /// </summary>
    internal interface INpmPackage
    {
        /// <summary>
        /// Gets or sets name of Npm object
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        string Version { get; set; }
    }

    /// <summary>
    /// INpmPackage plus version range string
    /// </summary>
    internal interface INpmPackageDependency : INpmPackage
    {
        /// <summary>
        /// Gets or sets the version range of supported dependency
        /// </summary>
        string VersionRange { get; set; }
    }

    /// <summary>
    /// INpmPackage plus dependencies for installed package
    /// </summary>
    internal interface INpmInstalledPackage : INpmPackage
    {
        /// <summary>
        /// Gets or sets the set of installed dependencies
        /// </summary>
        IEnumerable<INpmInstalledPackage> InstalledDependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of dependencies that are not installed
        /// </summary>
        IEnumerable<INpmPackageDependency> MissingDependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of dependencies that need to be updated
        /// </summary>
        IEnumerable<INpmPackageDependency> OutdatedDependencies { get; set; }
    }

    /// <summary>
    /// INpmPackage on remote repository. Most properties are optional
    /// </summary>
    internal interface INpmRemotePackage : INpmPackage
    {
        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the published versions
        /// </summary>
        IEnumerable<string> Versions { get; set; }

        /// <summary>
        /// Gets or sets the names of maintainers
        /// </summary>
        IEnumerable<string> Maintainers { get; set; }

        /// <summary>
        /// Gets or sets the names of contributors
        /// </summary>
        IEnumerable<string> Contributors { get; set; }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        IEnumerable<string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the URL for home page of project
        /// </summary>
        string HomepageUrl { get; set; }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// Gets or sets the set of required dependencies
        /// </summary>
        IEnumerable<INpmPackageDependency> Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of development dependencies
        /// </summary>
        IEnumerable<INpmPackageDependency> DevDependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of optional dependencies
        /// </summary>
        IEnumerable<INpmPackageDependency> OptionalDependencies { get; set; }

        /// <summary>
        /// Gets or sets the reference to license
        /// </summary>
        INpmReference License { get; set; }

        /// <summary>
        /// Gets or sets the reference to remote repository where it is published
        /// </summary>
        INpmReference Repository { get; set; }
    }

    /// <summary>
    /// INpmPackage plus properties from search result
    /// </summary>
    internal interface INpmSearchResultPackage : INpmPackage
    {
        /// <summary>
        /// Gets the text description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the author of project
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets the keywords
        /// </summary>
        string[] Keywords { get; }

        /// <summary>
        /// Gets the date of last publish
        /// </summary>
        DateTime Date { get; }
    }
}
