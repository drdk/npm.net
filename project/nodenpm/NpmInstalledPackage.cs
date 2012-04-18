// -----------------------------------------------------------------------
// <copyright file="NpmInstalledPackage.cs" company="Microsoft">
// Class for some npm package manager Installed Package representation
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NpmPackage plus dependencies
    /// </summary>
    public class NpmInstalledPackage : INpmInstalledPackage, IEquatable<INpmInstalledPackage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmInstalledPackage" /> class.
        /// </summary>
        public NpmInstalledPackage()
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
        /// Gets or sets the '/' delimited parents for this installation
        /// </summary>
        public string DependentPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is missing
        /// </summary>
        public bool IsMissing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is outdated
        /// </summary>
        public bool IsOutdated
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package has dependencies
        /// </summary>
        public bool HasDependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmInstalledPackage package)
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

            if (this.DependentPath != package.DependentPath)
            {
                return false;
            }

            if (this.IsMissing != package.IsMissing)
            {
                return false;
            }

            if (this.IsOutdated != package.IsOutdated)
            {
                return false;
            }

            if (this.HasDependencies != package.HasDependencies)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Test if another object matches this one
        /// </summary>
        /// <param name="package">object to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as INpmInstalledPackage);
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

            if (this.DependentPath != null)
            {
                hash = hash ^ this.DependentPath.GetHashCode();
            }

            return hash;
        }
    }
}
