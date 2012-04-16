// -----------------------------------------------------------------------
// <copyright file="NpmRemotePackage.cs" company="Microsoft">
// Class for some npm package manager Remote Package representation
// </copyright>
// -----------------------------------------------------------------------

namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Text;

    /// <summary>
    /// NpmPackage plus properties about the package as stored in the repository
    /// </summary>
    public class NpmRemotePackage : INpmRemotePackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmRemotePackage" /> class.
        /// </summary>
        public NpmRemotePackage()
        {
        }

        /// <summary>
        /// Gets or sets name of Npm object
        /// </summary>
        public string Name
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        public string Description
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the published versions
        /// </summary>
        public IEnumerable<string> Versions
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the names of maintainers
        /// </summary>
        public IEnumerable<string> Maintainers
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the names of contributors
        /// </summary>
        public IEnumerable<string> Contributors
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        public IEnumerable<string> Keywords
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the URL for home page of project
        /// </summary>
        public string Homepage
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        public string Author
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the set of required dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> Dependencies
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the set of development dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> DevDependencies
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the set of optional dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> OptionalDependencies
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the reference to license
        /// </summary>
        public INpmReference License
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the reference to remote repository where it is published
        /// </summary>
        public INpmReference Repository
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmRemotePackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmRemotePackage package)
        {
            if (package == null)
            {
                return false;
            }

            if (this.Name != package.Name)
            {
                return false;
            }

            if (this.Version != package.Version)
            {
                return false;
            }

            if (this.Description != package.Description)
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Versions, package.Versions))
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Maintainers, package.Maintainers))
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Contributors, package.Contributors))
            {
                return false;
            }

            if (this.Homepage != package.Homepage)
            {
                return false;
            }

            if (this.Author != package.Author)
            {
                return false;
            }

            // TODO compare dependencies, License, repository
            return true;
        }
    }
}
