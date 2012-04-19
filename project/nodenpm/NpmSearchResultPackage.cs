// -----------------------------------------------------------------------
// <copyright file="NpmSearchResultPackage.cs" company="Microsoft">
// Class for some npm package manager Search Result representation
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// NpmPackage plus properties from search result
    /// </summary>
    public class NpmSearchResultPackage : INpmSearchResultPackage, IEquatable<INpmSearchResultPackage>
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
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the version of Npm object if known
        /// </summary>
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        public IEnumerable<string> Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date of last publish
        /// </summary>
        public DateTime LatestDate
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmSearchResultPackage other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.Name != other.Name)
            {
                return false;
            }

            if (this.Description != other.Description)
            {
                return false;
            }

            if (this.Author != other.Author)
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(this.Keywords, other.Keywords))
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
            return this.Equals(obj as INpmSearchResultPackage);
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

            if (this.Description != null)
            {
                hash = hash ^ this.Description.GetHashCode();
            }

            if (this.Author != null)
            {
                hash = hash ^ this.Author.GetHashCode();
            }

            return hash;
        }
    }
}
