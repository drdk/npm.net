// -----------------------------------------------------------------------
// <copyright file="NpmPackageDependency.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NpmPackage plus optional version range
    /// </summary>
    internal class NpmPackageDependency : INpmPackageDependency
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the version range of supported dependency
        /// </summary>
        public string VersionRange { get; set; }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmInstalledPackeag to compare</param>
        /// <param name="diff">Output string has name of first mismatch</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmPackageDependency package, out string diff)
        {
            diff = string.Empty;
            if (this.Name != package.Name)
            {
                diff = "Name";
                return false;
            }

            if (this.Version != package.Version)
            {
                diff = "Version";
                return false;
            }

            if (this.VersionRange != package.VersionRange)
            {
                diff = "VersionRange";
                return false;
            }

            return true;
        }
    }
}
