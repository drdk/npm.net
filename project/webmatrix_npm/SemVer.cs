using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webmatrix_Npm
{
    internal class SemVer
    {
        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Patch { get; private set; }
        public string PreRelease { get; private set; }
        public string Build { get; private set; }
        public string Version { get; private set; }

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

        public SemVer(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Version = string.Format("{0}.{1}.{2}", major, minor, patch);
            Build = null;
            PreRelease = null;
        }

        public SemVer(int major, int minor, int patch, bool isBuild, string buildOrPrerel)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Build = null;
            PreRelease = null;
            if (isBuild)
            {
                Build = buildOrPrerel;
                Version = string.Format("{0}.{1}.{2}+{3}", major, minor, patch, buildOrPrerel);
            }
            else
            {
                PreRelease = buildOrPrerel;
                Version = string.Format("{0}.{1}.{2}-{3}", major, minor, patch, buildOrPrerel);
            }
        }

        public int CompareTo(SemVer semver)
        {
            if (Major < semver.Major)
            {
                return -1;
            }
            if (Major > semver.Major)
            {
                return 1;
            }
            if (Minor < semver.Minor)
            {
                return -1;
            }
            if (Minor > semver.Minor)
            {
                return 1;
            }
            if (Patch < semver.Patch)
            {
                return -1;
            }
            if (Patch > semver.Patch)
            {
                return 1;
            }
            if (PreRelease != null && semver.PreRelease == null)
            {
                return -1;
            }
            if (PreRelease == null && semver.PreRelease != null)
            {
                return 1;
            }
            if (Build != null && semver.Build == null)
            {
                return 1;
            }
            if (Build == null && semver.Build != null)
            {
                return -1;
            }

            // TODO - handle precedence if both have prerelease or build
            return 0;
        }
    }
}
