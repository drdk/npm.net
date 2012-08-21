// -----------------------------------------------------------------------
// <copyright file="NpmPackage.cs" company="Microsoft Open Technologies, Inc.">
// Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0.
//
// THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT
// LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR
// A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache License, Version 2.0 for specific language governing
// permissions and limitations under the License.
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NpmPackage has name and optional version
    /// </summary>
    public class NpmPackage : INpmPackage, IEquatable<INpmPackage>
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
            get;
            set;
        }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="other">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmPackage other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.Name != other.Name)
            {
                return false;
            }

            if (this.Version != other.Version)
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
            return this.Equals(obj as INpmPackage);
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

            if (this.Version != null)
            {
                hash = hash ^ this.Version.GetHashCode();
            }

            return hash;
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
                if (first.Count() !=
                    second.Count())
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
