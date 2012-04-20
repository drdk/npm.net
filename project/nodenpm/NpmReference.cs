// -----------------------------------------------------------------------
// <copyright file="NpmReference.cs" company="Microsoft">
// Class for npm package manager reference data
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NpmReference has type and a reference url
    /// </summary>
    public class NpmReference : INpmReference, IEquatable<INpmReference>
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
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reference URL
        /// </summary>
        public string Reference
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="other">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmReference other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.Type != other.Type)
            {
                return false;
            }

            if (this.Reference != other.Reference)
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
            return this.Equals(obj as INpmReference);
        }

        /// <summary>
        /// Calculate hash code
        /// </summary>
        /// <returns>hash value for object</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            if (this.Type != null)
            {
                hash = hash ^ this.Type.GetHashCode();
            }

            if (this.Reference != null)
            {
                hash = hash ^ this.Reference.GetHashCode();
            }

            return hash;
        }
    }
}
