// -----------------------------------------------------------------------
// <copyright file="NpmPackageDependency.cs" company="Microsoft">
// Class for some npm package manager Package Dependency representation
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
    /// NpmPackage plus optional version range
    /// </summary>
    [SecurityCritical]
    public class NpmPackageDependency : INpmPackageDependency
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
        /// Gets or sets the version range of supported dependency
        /// </summary>
        public string VersionRange
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmInstalledPackeag to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmPackageDependency package)
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

            if (this.VersionRange != package.VersionRange)
            {
                return false;
            }

            return true;
        }
    }
}
