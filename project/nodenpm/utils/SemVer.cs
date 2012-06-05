// -----------------------------------------------------------------------
// <copyright file="SemVer.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class for handling semver compatible version strings
    /// </summary>
    internal class SemVer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SemVer" /> class.
        /// </summary>
        /// <param name="major">Major number</param>
        /// <param name="minor">Minor number</param>
        /// <param name="patch">Patch number</param>
        public SemVer(int major, int minor, int patch)
        {
            this.Major = major;
            this.Minor = minor;
            this.Patch = patch;
            this.Version = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}", major, minor, patch);
            this.Build = null;
            this.PreRelease = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemVer" /> class.
        /// </summary>
        /// <param name="major">Major number</param>
        /// <param name="minor">Minor number</param>
        /// <param name="patch">Patch number</param>
        /// <param name="isBuild">true if build string, false if PreRelease</param>
        /// <param name="buildOrPrerel">String with build or prerelease</param>
        public SemVer(int major, int minor, int patch, bool isBuild, string buildOrPrerel)
        {
            this.Major = major;
            this.Minor = minor;
            this.Patch = patch;
            this.Build = null;
            this.PreRelease = null;
            if (isBuild)
            {
                this.Build = buildOrPrerel;
                this.Version = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}+{3}", major, minor, patch, buildOrPrerel);
            }
            else
            {
                this.PreRelease = buildOrPrerel;
                this.Version = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}-{3}", major, minor, patch, buildOrPrerel);
            }
        }

        /// <summary>
        /// Gets the major part of version number
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        /// Gets the minor part of version number
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// Gets the patch part of version number
        /// </summary>
        public int Patch { get; private set; }

        /// <summary>
        /// Gets the preRelease part of version if any
        /// </summary>
        public string PreRelease { get; private set; }

        /// <summary>
        /// Gets the build part of version if any
        /// </summary>
        public string Build { get; private set; }

        /// <summary>
        /// Gets the string representation of version
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Parse a semver campatable version string and create SemVer
        /// </summary>
        /// <param name="version">Version string</param>
        /// <returns>SemVer object</returns>
        /// <exception cref="ArgumentNullException">Version string may not be null</exception>
        /// <exception cref="ArgumentException">Invalid foramt for version string</exception>
        public static SemVer Parse(string version)
        {
            SemVer semver;
            int rc = ParseHelper(version, out semver);
            if (rc == 0)
            {
                return semver;
            }

            switch (rc)
            {
                case -1:
                    throw new ArgumentNullException("version");
                case -3:
                    throw new ArgumentException("Version Major component must be numeric");
                case -4:
                    throw new ArgumentException("Version Minor component must be numeric");
                case -5:
                    throw new ArgumentException("Version Patch component must be numeric");
                default:
                    throw new ArgumentException("Failure to parse version string");
            }
        }

        /// <summary>
        /// Try to parse a semver campatable version string and create SemVer
        /// </summary>
        /// <param name="version">version string</param>
        /// <param name="semver">created class</param>
        /// <returns>true if success, false if fails</returns>
        public static bool TryParse(string version, out SemVer semver)
        {
            int rc = ParseHelper(version, out semver);
            if (rc == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Compare this version with parameter
        /// </summary>
        /// <param name="semver">Semver to compare with</param>
        /// <returns>-1 if less than, 0 if equals, 1 if greater</returns>
        public int CompareTo(SemVer semver)
        {
            if (this.Major < semver.Major)
            {
                return -1;
            }

            if (this.Major > semver.Major)
            {
                return 1;
            }

            if (this.Minor < semver.Minor)
            {
                return -1;
            }

            if (this.Minor > semver.Minor)
            {
                return 1;
            }

            if (this.Patch < semver.Patch)
            {
                return -1;
            }

            if (this.Patch > semver.Patch)
            {
                return 1;
            }

            if (this.PreRelease != null && semver.PreRelease == null)
            {
                return -1;
            }

            if (this.PreRelease == null && semver.PreRelease != null)
            {
                return 1;
            }

            if (this.Build != null && semver.Build == null)
            {
                return 1;
            }

            if (this.Build == null && semver.Build != null)
            {
                return -1;
            }

            // TODO - handle precedence if both have prerelease or build
            return 0;
        }

        /// <summary>
        /// Parse a semver campatable version string and create SemVer
        /// </summary>
        /// <param name="version">version string</param>
        /// <param name="semver">created class</param>
        /// <returns>0 if success, negative if parse error</returns>
        private static int ParseHelper(string version, out SemVer semver)
        {
            int major = 0;
            int minor = 0;
            int patch = 0;
            semver = null;

            if (version == null)
            {
                return -1;
            }

            char[] seps1 = new char[1] { '.' };
            string[] parts = version.Split(seps1, 3);
            if (!int.TryParse(parts[0], out major))
            {
                return -3;
            }

            if (parts.Length > 1)
            {
                if (!int.TryParse(parts[1], out minor))
                {
                    return -4;
                }
            }

            if (parts.Length > 2)
            {
                char[] seps2 = new char[2] { '+', '-' };
                string[] patchparts = parts[2].Split(seps2, 2);
                if (!int.TryParse(patchparts[0], out patch))
                {
                    return -5;
                }

                if (patchparts.Length == 2)
                {
                    bool isBuild = false;
                    string ext = null;
                    if (parts[2].IndexOf('+') != -1)
                    {
                        isBuild = true;
                        ext = patchparts[1];
                    }
                    else
                    {
                        isBuild = false;
                        ext = patchparts[1];
                    }

                    semver = new SemVer(major, minor, patch, isBuild, ext);
                }
                else
                {
                    semver = new SemVer(major, minor, patch);
                }
            }
            else
            {
                semver = new SemVer(major, minor, patch);
            }

            return 0;
        }
    }
}
