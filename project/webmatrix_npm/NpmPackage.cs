namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// NpmPackage has name and optional version
    /// </summary>
    public class NpmPackage : INpmPackage
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
    }
}
