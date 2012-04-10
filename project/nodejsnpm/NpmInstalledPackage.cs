namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NpmPackage plus dependencies
    /// </summary>
    internal class NpmInstalledPackage : INpmInstalledPackage
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the set of installed dependencies
        /// </summary>
        public IEnumerable<INpmInstalledPackage> InstalledDependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of dependencies that are not installed
        /// </summary>
        public IEnumerable<INpmPackageDependency> MissingDependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of dependencies that need to be updated
        /// </summary>
        public IEnumerable<INpmPackageDependency> OutdatedDependencies { get; set; }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmInstalledPackeag to compare</param>
        /// <param name="diff">Output string has name of first mismatch</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmInstalledPackage package, out string diff)
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

            if ((this.InstalledDependencies == null && package.InstalledDependencies != null) ||
                (this.InstalledDependencies != null && package.InstalledDependencies == null))
            {
                diff = "InstalledDependencies";
                return false;
            }

            if (this.InstalledDependencies != null)
            {
                if (this.InstalledDependencies.Count<INpmInstalledPackage>() !=
                    package.InstalledDependencies.Count<INpmInstalledPackage>())
                {
                    diff = "InstalledDependencies";
                    return false;
                }
                
                // TODO compare members of InstalledDependencies
            }

            if ((this.MissingDependencies == null && package.MissingDependencies != null) ||
                (this.MissingDependencies != null && package.MissingDependencies == null))
            {
                diff = "MissingDependencies";
                return false;
            }

            if (this.MissingDependencies != null)
            {
                if (this.MissingDependencies.Count<INpmPackageDependency>() !=
                    package.MissingDependencies.Count<INpmPackageDependency>())
                {
                    diff = "MissingDependencies";
                    return false;
                }

                // TODO compare members of MissingDependencies
            }

            if ((this.OutdatedDependencies == null && package.OutdatedDependencies != null) ||
                (this.OutdatedDependencies != null && package.OutdatedDependencies == null))
            {
                diff = "OutdatedDependencies";
                return false;
            }

            if (this.OutdatedDependencies != null)
            {
                if (this.OutdatedDependencies.Count<INpmPackageDependency>() !=
                    package.OutdatedDependencies.Count<INpmPackageDependency>())
                {
                    diff = "OutdatedDependencies";
                    return false;
                }

                // TODO compare members of OutdatedDependencies
            }

            return true;
        }
    }
}
