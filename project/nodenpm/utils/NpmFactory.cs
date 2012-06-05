// -----------------------------------------------------------------------
// <copyright file="NpmFactory.cs" company="Microsoft Open Technologies, Inc.">
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
