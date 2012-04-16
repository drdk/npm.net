// -----------------------------------------------------------------------
// <copyright file="NpmReference.cs" company="Microsoft">
// Class for npm package manager reference data
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
    /// NpmReference has type and a reference url
    /// </summary>
    public class NpmReference : INpmReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmReference" /> class.
        /// </summary>
        /// <param name="type">The type string for the reference</param>
        /// <param name="reference">The url of the reference</param>
        public NpmReference(string type, string reference)
        {
            this.Type = type;
            this.Reference = reference;
        }

        /// <summary>
        /// Gets or sets the reference type
        /// </summary>
        public string Type
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets the reference URL
        /// </summary>
        public string Reference
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmReference to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmReference package)
        {
            if (package == null)
            {
                return false;
            }

            if (this.Type != package.Type)
            {
                return false;
            }

            if (this.Reference != package.Reference)
            {
                return false;
            }

            return true;
        }
    }
}
