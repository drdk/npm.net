namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// NpmPackage has name and optional version
    /// </summary>
    internal class NpmPackage : INpmPackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmPackage" /> class.
        /// </summary>
        /// <param name="name">name of package</param>
        /// <param name="version">version of package</param>
        public NpmPackage(string name, string version)
        {
            this.Name = name;
            this.Version = version;
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
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmPackage to compare</param>
        /// <param name="diff">Output string has name of first mismatch</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmPackage package, out string diff)
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

            return true;
        }
    }
}
