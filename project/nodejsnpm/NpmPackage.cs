// -----------------------------------------------------------------------
// <copyright file="NpmPackage.cs" company="Microsoft">
// Class for some npm package manager Package representation
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
        public string Name
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version
        {
            [SecurityCritical]
            get;
            [SecurityCritical]
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool IsSame(INpmPackage package)
        {
            if (package == null)
            {
                return false;
            }

            if (this.Name != package.Name)
            {
                return false;
            }

            if (this.Version != package.Version)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Compare two string enumerations for equality 
        /// </summary>
        /// <param name="first">string enumeration</param>
        /// <param name="second">second string enumeration to compare</param>
        /// <returns>true if match, false if not matched</returns>
        internal static bool IsSameStringEnumeration(
                                    IEnumerable<string> first,
                                    IEnumerable<string> second)
        {
            if (first != null && second != null)
            {
                if (first.Count<string>() !=
                    second.Count<string>())
                {
                    return false;
                }

                foreach (string member in second)
                {
                    if (!first.Contains(member))
                    {
                        return false;
                    }
                }

                return true;
            }

            if (first == null && second == null)
            {
                return true;
            }

            return false;
        }
    }
}
