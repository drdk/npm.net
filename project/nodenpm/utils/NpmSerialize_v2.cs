// -----------------------------------------------------------------------
// <copyright file="NpmSerialize_v2.cs" company="Microsoft Open Technologies, Inc.">
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

using System.Text.RegularExpressions;

namespace NodeNpm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web.Script.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NpmSerialize_v2 : NpmSerialize, INpmSerialize
    {
        /// <summary>
        /// convert npm install output to INpmInstalledPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumerable INpmInstalledPackage properties</returns>
        public new IEnumerable<INpmInstalledPackage> FromInstall(string output)
        {
            string wrapOutput;

            // sometimes the output has some text lines pre-pended, look for the first
            // json delimiter
            var regex = new Regex(@"\[\s*\{");
            Match m = regex.Match(output);
            
            int firstDelimiter = m.Index; output.IndexOfAny(new char[] { '[', '{' });
            if (firstDelimiter > 0)
            {
                output = output.Substring(firstDelimiter);
            }

            if (output[0] == '[')
            {
                // returns array without being wrapped in object.
                // wrap this in an object to allow json parser to work
                wrapOutput = "{ INSTALLROOT: " + output + " }";
            }
            else
            {
                wrapOutput = output;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<INpmInstalledPackage> installed = new List<INpmInstalledPackage>();
            Dictionary<string, object> listObj = null;

            try
            {
                listObj = serializer.Deserialize<Dictionary<string, object>>(wrapOutput);
            }
            catch (InvalidOperationException ex)
            {
                throw new NpmException(ParseErrorList, ex);
            }
            catch (ArgumentException ex)
            {
                throw new NpmException(ParseErrorList, ex);
            }

            try
            {
                if (listObj != null && listObj.Count > 0)
                {
                    object root = null;
                    listObj.TryGetValue("INSTALLROOT", out root);
                    ArrayList rootList = root as ArrayList;
                    if (rootList != null)
                    {
                        foreach (object topInstall in rootList)
                        {
                            Dictionary<string, object> topDict = topInstall as Dictionary<string, object>;
                            if (topDict != null)
                            {
                                object name = string.Empty;
                                topDict.TryGetValue("name", out name);
                                this.InstalledPackageFromDictionary(installed, name as string, string.Empty, topDict);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NpmException(ConvertErrorList, ex);
            }

            return installed;
        }
    }
}
