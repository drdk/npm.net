// -----------------------------------------------------------------------
// <copyright file="NpmInstalledPackage.cs" company="Microsoft">
// Class for some npm package manager Installed Package representation
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
    /// NpmPackage plus dependencies
    /// </summary>
    public class NpmInstalledPackage : INpmInstalledPackage
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
        /// Gets or sets the '/' delimited parents for this installation
        /// </summary>
        public string DependentPath
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is missing
        /// </summary>
        public bool IsMissing
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is outdated
        /// </summary>
        public bool IsOutdated
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package has dependencies
        /// </summary>
        public bool HasDependencies
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
        public bool IsSame(INpmInstalledPackage package)
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

            return true;
        }
    }
}
