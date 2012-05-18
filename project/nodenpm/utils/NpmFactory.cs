// -----------------------------------------------------------------------
// <copyright file="NpmFactory.cs" company="Microsoft">
// Class for npm package manager class factory
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Factory to return client and serialize objects for current version
    /// </summary>
    public class NpmFactory
    {
        /// <summary>
        /// Get NpmClient to support the version.
        /// </summary>
        /// <param name="version">npm version string or null for default</param>
        /// <returns>INpmClient instance</returns>
        public virtual INpmClient GetClient(string version)
        {
            return new NpmClient();
        }

        /// <summary>
        /// GetNpmSerialize to support the version
        /// </summary>
        /// <param name="version">npm version string or null for default</param>
        /// <returns>INpmSerialize instance</returns>
        public virtual INpmSerialize GetSerialize(string version)
        {
            SemVer ver;
            if (version == null)
            {
                // return latest
                return new NpmSerialize_v2();
            }
            else
            {
                ver = SemVer.Parse(version);
            }

            // Data output for install changed in 1.1.14
            if (ver.CompareTo(new SemVer(1, 1, 14)) >= 0)
            {
                return new NpmSerialize_v2();
            }
            else
            {
                return new NpmSerialize();
            }
        }
    }
}
