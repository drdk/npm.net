namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// NpmPackage plus properties from search result
    /// </summary>
    internal class NpmSearchResultPackage : INpmSearchResultPackage
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
            this.Date = date;
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
        /// Gets the text description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the author of project
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Gets the keywords
        /// </summary>
        public string[] Keywords { get; private set; }

        /// <summary>
        /// Gets the date of last publish
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmSearchResultPackage to compare</param>
        /// <param name="diff">Output string has name of first mismatch</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmSearchResultPackage package, out string diff)
        {
            diff = string.Empty;

            if (this.Name != package.Name)
            {
                diff = "Name";
                return false;
            }

            if (this.Description != package.Description)
            {
                diff = "Difference";
                return false;
            }

            if (this.Author != package.Author)
            {
                diff = "Author";
                return false;
            }

            if ((this.Keywords != null && package.Keywords == null) ||
                (this.Keywords == null && package.Keywords != null))
            {
                diff = "Keywords";
                return false;
            }

            if (this.Keywords != null)
            {
                if (this.Keywords.Length != package.Keywords.Length)
                {
                    diff = "Keywords";
                    return false;
                }

                // TODO compare each keyword
            }

            return true;
        }
    }
}
