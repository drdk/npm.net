// -----------------------------------------------------------------------
// <copyright file="INpmPackage.cs" company="Microsoft">
// Interface for some npm package manager data classes
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// INpmPackage has name and optional version
    /// </summary>
    public interface INpmPackage
    {
        /// <summary>
        /// Gets or sets name of Npm object
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        string Version
        {
            get;
            set;
        }
    }

    /// <summary>
    /// INpmPackage plus version range string
    /// </summary>
    public interface INpmPackageDependency : INpmPackage
    {
        /// <summary>
        /// Gets or sets the version range of supported dependency
        /// </summary>
        string VersionRange
        {
            get;
            set;
        }
    }

    /// <summary>
    /// INpmPackage plus dependencies for installed package
    /// </summary>
    public interface INpmInstalledPackage : INpmPackage
    {
        /// <summary>
        /// Gets or sets the '/' delimited parents for this installation
        /// </summary>
        string DependentPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is missing
        /// </summary>
        bool IsMissing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is outdated
        /// </summary>
        bool IsOutdated
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package has dependencies
        /// </summary>
        bool HasDependencies
        {
            get;
            set;
        }
    }

    /// <summary>
    /// INpmPackage on remote repository. Most properties are optional
    /// </summary>
    public interface INpmRemotePackage : INpmPackage
    {
        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the published versions
        /// </summary>
        IEnumerable<string> Versions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the names of maintainers
        /// </summary>
        IEnumerable<string> Maintainers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the names of contributors
        /// </summary>
        IEnumerable<string> Contributors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        IEnumerable<string> Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the URL for home page of project
        /// </summary>
        string Homepage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of required dependencies
        /// </summary>
        IEnumerable<INpmPackageDependency> Dependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of development dependencies
        /// </summary>
        IEnumerable<INpmPackageDependency> DevDependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of optional dependencies
        /// </summary>
        IEnumerable<INpmPackageDependency> OptionalDependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reference to license
        /// </summary>
        INpmReference License
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reference to remote repository where it is published
        /// </summary>
        INpmReference Repository
        {
            get;
            set;
        }
    }

    /// <summary>
    /// INpmPackage plus properties from search result
    /// </summary>
    public interface INpmSearchResultPackage : INpmPackage
    {
        /// <summary>
        /// Gets the text description
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// Gets the author of project
        /// </summary>
        string Author
        {
            get;
        }

        /// <summary>
        /// Gets the keywords
        /// </summary>
        IEnumerable<string> Keywords
        {
            get;
        }

        /// <summary>
        /// Gets the date of last publish
        /// </summary>
        DateTime LatestDate
        {
            get;
        }
    }

    /// <summary>
    /// Npm reference has a type and URL
    /// </summary>
    public interface INpmReference
    {
        /// <summary>
        /// Gets or sets the reference type
        /// </summary>
        string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reference URL
        /// </summary>
        string Reference
        {
            get;
            set;
        }
    }
}
