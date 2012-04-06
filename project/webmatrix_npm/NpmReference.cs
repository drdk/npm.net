// -----------------------------------------------------------------------
// <copyright file="NpmReference.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class NpmReference : INpmReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmReference" /> class.
        /// </summary>
        /// <param name="type">The type string for the reference</param>
        /// <param name="url">The url of the reference</param>
        public NpmReference(string type, string url)
        {
            this.Type = type;
            this.Url = url;
        }

        /// <summary>
        /// Gets or sets the reference type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the reference URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmReference to compare</param>
        /// <param name="diff">Output string has name of first mismatch</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmReference package, out string diff)
        {
            diff = string.Empty;
            if (this.Type != package.Type)
            {
                diff = "Type";
                return false;
            }

            if (this.Url != package.Url)
            {
                diff = "Url";
                return false;
            }

            return true;
        }
    }
}
