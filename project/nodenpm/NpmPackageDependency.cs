// -----------------------------------------------------------------------
// <copyright file="NpmPackageDependency.cs" company="Microsoft">
// Class for some npm package manager Package Dependency representation
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NpmPackage plus optional version range
    /// </summary>
    public class NpmPackageDependency : INpmPackageDependency, IEquatable<INpmPackageDependency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmPackageDependency" /> class.
        /// </summary>
        public NpmPackageDependency()
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
        /// Gets or sets the version range of supported dependency
        /// </summary>
        public string VersionRange
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="other">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmPackageDependency other)
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

            if (this.VersionRange != other.VersionRange)
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
            return this.Equals(obj as INpmPackageDependency);
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

            if (this.VersionRange != null)
            {
                hash = hash ^ this.VersionRange.GetHashCode();
            }

            return hash;
        }
    }
}
