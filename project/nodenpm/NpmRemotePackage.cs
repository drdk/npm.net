// -----------------------------------------------------------------------
// <copyright file="NpmRemotePackage.cs" company="Microsoft">
// Class for some npm package manager Remote Package representation
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NpmPackage plus properties about the package as stored in the repository
    /// </summary>
    public class NpmRemotePackage : INpmRemotePackage, IEquatable<INpmRemotePackage>
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
            get;
            set;
        }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the published versions
        /// </summary>
        public IEnumerable<string> Versions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the names of maintainers
        /// </summary>
        public IEnumerable<string> Maintainers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the names of contributors
        /// </summary>
        public IEnumerable<string> Contributors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        public IEnumerable<string> Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the URL for home page of project
        /// </summary>
        public string Homepage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of required dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> Dependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of development dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> DevDependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of optional dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> OptionalDependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reference to license
        /// </summary>
        public INpmReference License
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reference to remote repository where it is published
        /// </summary>
        public INpmReference Repository
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="other">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmRemotePackage other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.Name != other.Name)
            {
                return false;
            }

            if (this.Version != other.Version)
            {
                return false;
            }

            if (this.Description != other.Description)
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Versions, other.Versions))
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Maintainers, other.Maintainers))
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Contributors, other.Contributors))
            {
                return false;
            }

            if (this.Homepage != other.Homepage)
            {
                return false;
            }

            if (this.Author != other.Author)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Test if another object matches this one
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as INpmRemotePackage);
        }

        /// <summary>
        /// Calculate hash code
        /// </summary>
        /// <returns>hash value for object</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            if (this.Name != null)
            {
                hash = hash ^ this.Name.GetHashCode();
            }

            if (this.Version != null)
            {
                hash = hash ^ this.Version.GetHashCode();
            }

            return hash;
        }
    }
}
