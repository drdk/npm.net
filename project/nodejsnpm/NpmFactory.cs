namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Factory to return client and serialize objects for current version
    /// </summary>
    internal class NpmFactory
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
            return new NpmSerialize();
        }
    }
}
