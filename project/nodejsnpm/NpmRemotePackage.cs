// -----------------------------------------------------------------------
// <copyright file="NpmRemotePackage.cs" company="">
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
    /// TODO: Update summary.
    /// </summary>
    internal class NpmRemotePackage : INpmRemotePackage
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the published versions
        /// </summary>
        public IEnumerable<string> Versions { get; set; }

        /// <summary>
        /// Gets or sets the names of maintainers
        /// </summary>
        public IEnumerable<string> Maintainers { get; set; }

        /// <summary>
        /// Gets or sets the names of contributors
        /// </summary>
        public IEnumerable<string> Contributors { get; set; }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        public IEnumerable<string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the URL for home page of project
        /// </summary>
        public string HomepageUrl { get; set; }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the set of required dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of development dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> DevDependencies { get; set; }

        /// <summary>
        /// Gets or sets the set of optional dependencies
        /// </summary>
        public IEnumerable<INpmPackageDependency> OptionalDependencies { get; set; }

        /// <summary>
        /// Gets or sets the reference to license
        /// </summary>
        public INpmReference License { get; set; }

        /// <summary>
        /// Gets or sets the reference to remote repository where it is published
        /// </summary>
        public INpmReference Repository { get; set; }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmRemotePackage to compare</param>
        /// <param name="diff">Output string has name of first mismatch</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmRemotePackage package, out string diff)
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

            if (this.Description != package.Description)
            {
                diff = "Description";
                return false;
            }

            if ((this.Versions != null && package.Versions == null) ||
                (this.Versions == null && package.Versions != null))
            {
                diff = "Versions";
                return false;
            }

            if (this.Versions != null)
            {
                if (this.Versions.Count<string>() !=
                    package.Versions.Count<string>())
                {
                    diff = "Versions";
                    return false;
                }

                foreach (string version in package.Versions)
                {
                    if (!this.Versions.Contains(version))
                    {
                        diff = "Versions";
                        return false;
                    }
                }
            }

            if ((this.Maintainers != null && package.Maintainers == null) ||
                (this.Maintainers == null && package.Maintainers != null))
            {
                diff = "Maintainers";
                return false;
            }

            if (this.Maintainers != null)
            {
                if (this.Maintainers.Count<string>() !=
                    package.Maintainers.Count<string>())
                {
                    diff = "Maintainers";
                    return false;
                }

                foreach (string maintainer in package.Maintainers)
                {
                    if (!this.Maintainers.Contains(maintainer))
                    {
                        diff = "Maintainers";
                        return false;
                    }
                }
            }

            if ((this.Contributors != null && package.Contributors == null) ||
                (this.Contributors == null && package.Contributors != null))
            {
                diff = "Contributors";
                return false;
            }

            if (this.Contributors != null)
            {
                if (this.Contributors.Count<string>() !=
                    package.Contributors.Count<string>())
                {
                    diff = "Contributors";
                    return false;
                }

                foreach (string contributor in package.Contributors)
                {
                    if (!this.Contributors.Contains(contributor))
                    {
                        diff = "Contributors";
                        return false;
                    }
                }
            }

            if (this.HomepageUrl != package.HomepageUrl)
            {
                diff = "HomepageUrl";
                return false;
            }

            if (this.Author != package.Author)
            {
                diff = "Author";
                return false;
            }

            // TODO compare dependencies, License, repository
            return true;
        }
    }
}
