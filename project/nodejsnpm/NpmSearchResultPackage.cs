// -----------------------------------------------------------------------
// <copyright file="NpmSearchResultPackage.cs" company="Microsoft">
// Class for some npm package manager Search Result representation
// </copyright>
// -----------------------------------------------------------------------

namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Security;
    using System.Text;

    /// <summary>
    /// NpmPackage plus properties from search result
    /// </summary>
    public class NpmSearchResultPackage : INpmSearchResultPackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmSearchResultPackage" /> class.
        /// </summary>
        /// <param name="name">name of package</param>
        /// <param name="version">version of package</param>
        /// <param name="description">description of package</param>
        /// <param name="author">author of package</param>
        /// <param name="date">date of package</param>
        /// <param name="keywords">keywords for package</param>
        public NpmSearchResultPackage(
                                      string name,
                                      string version,
                                      string description,
                                      string author,
                                      DateTime date,
                                      string[] keywords)
        {
            this.Name = name;
            this.Version = version;
            this.Description = description;
            this.Author = author;
            this.Keywords = keywords;
            this.LatestDate = date;
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
        /// Gets or sets the version of Npm object if known
        /// </summary>
        public string Version
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        public string Description
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        public string Author
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        public IEnumerable<string> Keywords
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the date of last publish
        /// </summary>
        public DateTime LatestDate
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmSearchResultPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmSearchResultPackage package)
        {
            if (package == null)
            {
                return false;
            }

            if (this.Name != package.Name)
            {
                return false;
            }

            if (this.Description != package.Description)
            {
                return false;
            }

            if (this.Author != package.Author)
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Keywords, package.Keywords))
            {
                return false;
            }

            return true;
        }
    }
}
