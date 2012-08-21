// -----------------------------------------------------------------------
// <copyright file="NpmSerialize.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Script.Serialization;

    /// <summary>
    /// class for serialization from npm to objects
    /// </summary>
    public class NpmSerialize : INpmSerialize
    {
        /// <summary>
        /// Directory name inserted for each dependency level
        /// </summary>
        protected const string ModuleDir = "node_modules";

        /// <summary>
        /// Directory name inserted for each dependency level
        /// </summary>
        protected const string ModuleSeparator = "/node_modules/";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string ParseErrorInstall = "Failed: unable to parse output from install command.";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string ConvertErrorInstall = "Failed: unable to convert output from install command to object.";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string ParseErrorList = "Failed: unable to parse output from list command.";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string ConvertErrorList = "Failed: unable to convert output from list command to object.";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string ParseErrorView = "Failed: unable to parse output from view command.";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string ConvertErrorView = "Failed: unable to convert output from view command to object.";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string NpmReportedError = "Failed: npm reported an error.";

        /// <summary>
        /// Error message
        /// </summary>
        protected const string NpmErrorLineStart = "npm ERR! ";

        /// <summary>
        /// Regular expression for parsing search output
        /// </summary>
        protected const string SearchPattern = @"^(\S+)\s+(.*)=(\S+)\s+([0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2})\s*(.*)$";

        /// <summary>
        /// Regular expression for combined json strings
        /// </summary>
        protected const string ConcatJsonPattern = @"\r\n}\s*{";

        /// <summary>
        /// Regular expression class for parsing search output
        /// </summary>
        private static Regex searchRegex = null;

        /// <summary>
        /// Regular expression class for parsing search output
        /// </summary>
        private static Regex concatJsonRegex = null;

        /// <summary>
        /// Build path for specified dependency
        /// </summary>
        /// <param name="depends">dependency name</param>
        /// <returns>Relative path to dependency</returns>
        public static string ConvertDependToPath(string depends)
        {
            if (depends == null)
            {
                return null;
            }

            char[] seps = new char[] { '/' };
            string[] dependencies = depends.Split(seps);
            string relative = ".";

            // skip first part & keep adding module path to root
            for (int i = 1; i < dependencies.Length; i++)
            {
                relative = Path.Combine(relative, ModuleDir, dependencies[i]);
            }

            return relative;
        }

        /// <summary>
        /// Build path for specified dependency
        /// </summary>
        /// <param name="relativePath">module path</param>
        /// <returns>Path to dependency</returns>
        public static string ConvertPathToDepends(string relativePath)
        {
            if (relativePath == null)
            {
                return null;
            }

            string[] moduleSeparator = new string[] { ModuleSeparator };
            string[] modules = relativePath.Split(moduleSeparator, StringSplitOptions.RemoveEmptyEntries);
            string depends = string.Empty;

            foreach (string module in modules)
            {
                if (string.IsNullOrWhiteSpace(depends))
                {
                    depends = module;
                }
                else
                {
                    depends = depends + "/" + module;
                }
            }

            return depends;
        }

        /// <summary>
        /// converts npm list output to NpmInstalledPackage
        /// </summary>
        /// <param name="listJson">text output</param>
        /// <returns>enumerable NpmInstalledPackage properties</returns>
        public IEnumerable<INpmInstalledPackage> FromListInstalled(string listJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<INpmInstalledPackage> installed = new List<INpmInstalledPackage>();
            Dictionary<string, object> listObj = null;

            try
            {
                listObj = serializer.Deserialize<Dictionary<string, object>>(listJson);
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
                    object name = string.Empty;
                    listObj.TryGetValue("name", out name);
                    this.InstalledPackageFromDictionary(installed, name as string, string.Empty, listObj);
                }
            }
            catch (Exception ex)
            {
                throw new NpmException(ConvertErrorList, ex);
            }

            return installed;
        }

        /// <summary>
        /// converts npm list output to NpmInstalledPackage enumeration of immediate children
        /// </summary>
        /// <param name="listJson">text output</param>
        /// <returns>enumerable NpmInstalledPackage properties</returns>
        public IEnumerable<INpmInstalledPackage> FromListInstalledChildren(string listJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<INpmInstalledPackage> installed = new List<INpmInstalledPackage>();
            Dictionary<string, object> listObj = null;

            try
            {
                listObj = serializer.Deserialize<Dictionary<string, object>>(listJson);
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
                    object name;
                    listObj.TryGetValue("name", out name);
                    if (name == null)
                    {
                        name = ".";
                    }

                    this.InstalledDependenciesFromDictionary(installed, name as string, listObj);
                }
            }
            catch (Exception ex)
            {
                throw new NpmException(ConvertErrorList, ex);
            }

            return installed;
        }

        /// <summary>
        /// parse npm list output for matching NpmInstalledPackage
        /// </summary>
        /// <param name="listJson">text output</param>
        /// <param name="package">Installed package with name to match</param>
        /// <returns>NpmInstalledPackage properties or null</returns>
        public INpmInstalledPackage FromListMatchInstalled(string listJson, INpmPackage package)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            INpmInstalledPackage matched = null;
            Dictionary<string, object> listObj = null;

            try
            {
                listObj = serializer.Deserialize<Dictionary<string, object>>(listJson);
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
                    object name = string.Empty;
                    listObj.TryGetValue("name", out name);
                    matched = this.MatchPackageFromDictionary(package, name as string, string.Empty, listObj);
                }
            }
            catch (Exception ex)
            {
                throw new NpmException(ConvertErrorList, ex);
            }

            return matched;
        }

        /// <summary>
        /// convert npm view output to INpmRemotePackage
        /// </summary>
        /// <param name="viewJson">text output</param>
        /// <returns>INpmRemotePackage with property values</returns>
        public INpmRemotePackage FromView(string viewJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            INpmRemotePackage view = null;
            Dictionary<string, object> viewObj = null;

            try
            {
                viewObj = serializer.Deserialize<Dictionary<string, object>>(viewJson);
            }
            catch (InvalidOperationException ex)
            {
                throw new NpmException(ParseErrorView, ex);
            }
            catch (ArgumentException ex)
            {
                throw new NpmException(ParseErrorView, ex);
            }

            try
            {
                if (viewObj != null && viewObj.Count > 0)
                {
                    object name;
                    viewObj.TryGetValue("name", out name);
                    view = RemotePackageFromDictionary(name as string, viewObj);
                }
            }
            catch (Exception ex)
            {
                throw new NpmException(ConvertErrorView, ex);
            }

            return view;
        }

        /// <summary>
        /// convert npm outdated output to NpmPackageDependency enumeration
        /// </summary>
        /// <param name="outdated">text output</param>
        /// <returns>enumerable INpmPackageDependency properties</returns>
        public IEnumerable<INpmPackageDependency> FromOutdatedDependency(string outdated)
        {
            if (outdated == null)
            {
                return null;
            }

            List<INpmPackageDependency> depends = new List<INpmPackageDependency>();
            char[] seps = new char[] { '\n' };
            string[] lines = outdated.Trim().Split(seps, StringSplitOptions.RemoveEmptyEntries);

            seps = new char[] { ' ' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                int tokenCount = tokens.Length;

                if (tokenCount > 0)
                {
                    NpmPackageDependency dependency = new NpmPackageDependency();
                    seps = new char[] { '@' };
                    string[] nameVersion = tokens[0].Split(seps);
                    dependency.Name = nameVersion[0];
                    if (nameVersion.Length > 1)
                    {
                        dependency.VersionRange = nameVersion[1];
                    }

                    if (tokenCount > 2)
                    {
                        seps = new char[] { '=' };
                        string[] current = tokens[2].Split(seps);
                        if (current.Length > 1 && current[0] == "current" && current[1] != "MISSING")
                        {
                            dependency.Version = current[1];
                        }
                    }

                    depends.Add(dependency);
                }
            }

            return depends;
        }

        /// <summary>
        /// convert npm search output to INpmSearchResultPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumerable INpmSearchResultPackage properties</returns>
        public IEnumerable<INpmSearchResultPackage> FromSearchResult(string output)
        {
            List<INpmSearchResultPackage> results = new List<INpmSearchResultPackage>();
            if (output == null)
            {
                return results;
            }

            string[] lines = output.Split('\n');
            int count = lines.Length;

            if (searchRegex == null)
            {
                searchRegex = new Regex(SearchPattern, RegexOptions.Compiled);
            }

            // skip first line - header
            for (int ix = 1; ix < count; ix++)
            {
                string desc = null;
                string name = null;
                string author = null;
                string date = null;
                string keywordlist = null;
                DateTime dateParsed = DateTime.Now;
                string[] keywords = null;

                Match m = searchRegex.Match(lines[ix]);
                if (m.Success && m.Groups.Count > 1)
                {
                    name = m.Groups[1].ToString();
                    if (m.Groups.Count > 2)
                    {
                        desc = m.Groups[2].ToString().Trim();
                    }

                    if (m.Groups.Count > 3)
                    {
                        author = m.Groups[3].ToString().Trim();
                    }

                    if (m.Groups.Count > 4)
                    {
                        date = m.Groups[4].ToString();
                        if (!DateTime.TryParse(date, out dateParsed))
                        {
                            dateParsed = DateTime.Now;
                        }
                    }

                    if (m.Groups.Count > 5)
                    {
                        keywordlist = m.Groups[5].ToString();
                        keywords = keywordlist.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    NpmSearchResultPackage result = new NpmSearchResultPackage(
                                                                            name,
                                                                            null,
                                                                            desc,
                                                                            author,
                                                                            dateParsed,
                                                                            keywords);
                    results.Add(result);
                }
            }

            return (IEnumerable<INpmSearchResultPackage>)results;
        }

        /// <summary>
        /// convert npm install output to INpmInstalledPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumerable INpmInstalledPackage properties</returns>
        public IEnumerable<INpmInstalledPackage> FromInstall(string output)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<INpmInstalledPackage> installs = new List<INpmInstalledPackage>();
            Dictionary<string, object> installObj = null;

            if (concatJsonRegex == null)
            {
                concatJsonRegex = new Regex(ConcatJsonPattern, RegexOptions.Compiled);
            }

            // In case we get multiple Json object returned, split into multiple strings
            MatchCollection matches = concatJsonRegex.Matches(output);
            string[] jsonStrings = new string[matches.Count + 1];

            int startIndex = 0;
            int stringNum = 0;
            foreach (Match match in matches)
            {
                int nextIndex = match.Index + 3;
                jsonStrings[stringNum] = output.Substring(startIndex, nextIndex - startIndex);
                startIndex = nextIndex;
                stringNum++;
            }

            jsonStrings[stringNum] = output.Substring(startIndex);

            foreach (string jsonString in jsonStrings)
            {
                try
                {
                    installObj = serializer.Deserialize<Dictionary<string, object>>(jsonString);
                }
                catch (InvalidOperationException ex)
                {
                    throw new NpmException(ParseErrorInstall, ex);
                }
                catch (ArgumentException ex)
                {
                    throw new NpmException(ParseErrorInstall, ex);
                }

                try
                {
                    if (installObj != null && installObj.Count > 0)
                    {
                        foreach (KeyValuePair<string, object> module in installObj)
                        {
                            NpmSerialize.InstalledPackagesFromInstalledDictionary(installs, module.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new NpmException(ConvertErrorInstall, ex);
                }
            }

            return installs;
        }

        /// <summary>
        /// Creates an NpmException with properties from the error output
        /// </summary>
        /// <param name="output">The error stream output from npm</param>
        /// <returns>a new NpmException</returns>
        public NpmException ExceptionFromError(string output)
        {
            if (output == null)
            {
                return null;
            }

            NpmException exception = new NpmException(NpmReportedError);
            string[] errorLines = output.Split('\n');

            foreach (string line in errorLines)
            {
                if (line.StartsWith(NpmErrorLineStart, StringComparison.Ordinal))
                {
                    string errorText = line.Substring(NpmErrorLineStart.Length);
                    exception.SetNpmValue(errorText);
                }
            }

            return exception;
        }

        /// <summary>
        /// Set package name and version from string name@version 
        /// </summary>
        /// <param name="package">INpmPackage to update</param>
        /// <param name="nameVersion">string name or name@version</param>
        protected static void FillPackageFromNameVersion(INpmPackage package, string nameVersion)
        {
            if (nameVersion == null)
            {
                package.Name = null;
                package.Version = null;
            }
            else
            {
                char[] seps = new char[] { '@' };
                string[] nameAndVersion = nameVersion.Split(seps);
                if (nameAndVersion.Length > 1)
                {
                    package.Name = nameAndVersion[0];
                    package.Version = nameAndVersion[1];
                }
                else
                {
                    package.Name = nameAndVersion[0];
                    package.Version = null;
                }
            }
        }

        /// <summary>
        /// Build a remote package object from the parsed json
        /// </summary>
        /// <param name="name">name of package</param>
        /// <param name="viewObj">dictionary at root of object</param>
        /// <returns>NpmRemotePackage instance</returns>
        protected static NpmRemotePackage RemotePackageFromDictionary(string name, Dictionary<string, object> viewObj)
        {
            NpmRemotePackage remote = new NpmRemotePackage();
            remote.Name = name;
            if (viewObj.ContainsKey("dist-tags"))
            {
                Dictionary<string, object> dist = viewObj["dist-tags"] as Dictionary<string, object>;
                if (dist != null)
                {
                    if (dist.ContainsKey("stable"))
                    {
                        remote.Version = dist["stable"] as string;
                    }
                    else if (dist.ContainsKey("latest"))
                    {
                        remote.Version = dist["latest"] as string;
                    }
                }
            }

            if (viewObj.ContainsKey("description"))
            {
                remote.Description = viewObj["description"] as string;
            }

            if (viewObj.ContainsKey("homepage"))
            {
                remote.Homepage = viewObj["homepage"] as string;
            }

            if (viewObj.ContainsKey("author"))
            {
                remote.Author = viewObj["author"] as string;
            }

            if (viewObj.ContainsKey("versions"))
            {
                remote.Versions = ConvertStringArray(viewObj["versions"]);
            }

            if (viewObj.ContainsKey("maintainers"))
            {
                remote.Maintainers = ConvertStringArray(viewObj["maintainers"]);
            }

            if (viewObj.ContainsKey("contributors"))
            {
                remote.Contributors = ConvertStringArray(viewObj["contributors"]);
            }

            if (viewObj.ContainsKey("keywords"))
            {
                remote.Keywords = ConvertStringArray(viewObj["keywords"]);
            }

            if (viewObj.ContainsKey("dependencies"))
            {
                remote.Dependencies = ConvertDependencies(viewObj["dependencies"]);
            }

            if (viewObj.ContainsKey("devDependencies"))
            {
                remote.DevDependencies = ConvertDependencies(viewObj["devDependencies"]);
            }

            if (viewObj.ContainsKey("optionalDependencies"))
            {
                remote.OptionalDependencies = ConvertDependencies(viewObj["optionalDependencies"]);
            }

            if (viewObj.ContainsKey("licenses"))
            {
                remote.License = ConvertReference(viewObj["licenses"]);
            }

            if (viewObj.ContainsKey("repositories"))
            {
                remote.Repository = ConvertReference(viewObj["repositories"]);
            }

            return remote;
        }

        /// <summary>
        /// Convert a parsed json string or array of strings to a List
        /// </summary>
        /// <param name="obj">object to convert</param>
        /// <returns>A List of strings</returns>
        protected static List<string> ConvertStringArray(object obj)
        {
            List<string> strings = null;
            ArrayList array = obj as ArrayList;
            if (array != null)
            {
                strings = new List<string>(array.Count);
                foreach (object item in array)
                {
                    strings.Add(item as string);
                }

                return strings;
            }
            else
            {
                string val = obj as string;
                if (val != null)
                {
                    strings = new List<string>(1);
                    strings.Add(val);
                }
            }

            return strings;
        }

        /// <summary>
        /// Convert a parsed json dependency entry to a package dependency
        /// </summary>
        /// <param name="key">The string key of the dependency</param>
        /// <param name="value">The object value</param>
        /// <returns>A package dependency</returns>
        protected static NpmPackageDependency ConvertKeyValueToDependency(string key, object value)
        {
            NpmPackageDependency dependency = new NpmPackageDependency();
            dependency.Name = key;
            dependency.VersionRange = value as string;
            return dependency;
        }

        /// <summary>
        /// Convert a set of dependencies to a List
        /// </summary>
        /// <param name="obj">The parsed json object to convert</param>
        /// <returns>A List of package dependencies</returns>
        protected static List<NpmPackageDependency> ConvertDependencies(object obj)
        {
            Dictionary<string, object> deps = obj as Dictionary<string, object>;
            if (deps != null)
            {
                List<NpmPackageDependency> dependencies = new List<NpmPackageDependency>();
                foreach (KeyValuePair<string, object> pair in deps)
                {
                    NpmPackageDependency dependency = ConvertKeyValueToDependency(pair.Key, pair.Value);
                    dependencies.Add(dependency);
                }

                return dependencies;
            }

            return null;
        }

        /// <summary>
        /// Convert a parsed json object to a reference
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns>A reference with type and url</returns>
        protected static NpmReference ConvertReference(object obj)
        {
            Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
            if (dictionary != null)
            {
                if (dictionary.ContainsKey("type") && dictionary.ContainsKey("url"))
                {
                    return new NpmReference(dictionary["type"] as string, dictionary["url"] as string);
                }
            }
            else
            {
                string url = obj as string;
                if (url != null)
                {
                    return new NpmReference(string.Empty, url);
                }
            }

            return null;
        }

        /// <summary>
        /// Build installed package list from Dictionary based on install json
        /// </summary>
        /// <param name="parent">Parent list for this object</param>
        /// <param name="child">Child object is dictionary from json deserialize</param>
        protected static void InstalledPackagesFromInstalledDictionary(
                                            List<INpmInstalledPackage> parent,
                                            object child)
        {
            Dictionary<string, object> childObj = child as Dictionary<string, object>;
            if (childObj == null)
            {
                return;
            }

            NpmInstalledPackage installed = new NpmInstalledPackage();
            if (childObj.ContainsKey("what"))
            {
                NpmSerialize.FillPackageFromNameVersion(installed, childObj["what"] as string);
            }

            if (childObj.ContainsKey("parentDir"))
            {
                string parentDirectory = childObj["parentDir"] as string;
                if (parentDirectory != null)
                {
                    installed.DependentPath = NpmSerialize.ConvertPathToDepends(parentDirectory);
                }
            }
            else
            {
                installed.DependentPath = string.Empty;
            }

            installed.IsMissing = false;
            installed.IsOutdated = false;

            if (childObj.ContainsKey("children"))
            {
                ArrayList array = childObj["children"] as ArrayList;
                if (array != null && array.Count > 0)
                {
                    installed.HasDependencies = true;

                    foreach (object item in array)
                    {
                        NpmSerialize.InstalledPackagesFromInstalledDictionary(parent, item);
                    }
                }
            }

            parent.Add(installed);
        }

        /// <summary>
        /// Deserialize the parsed json results to a package
        /// </summary>
        /// <param name="parent">Parent list for this object</param>
        /// <param name="name">name of package</param>
        /// <param name="dependentPath">list of parents delimited by "/"</param>
        /// <param name="listObj">dictionary at root of package</param>
        /// <remarks>This is called recursively as a dependency is a package</remarks>
        protected void InstalledPackageFromDictionary(
                                                    List<INpmInstalledPackage> parent,
                                                    string name,
                                                    string dependentPath,
                                                    Dictionary<string, object> listObj)
        {
            NpmInstalledPackage installed = new NpmInstalledPackage();
            if (!string.IsNullOrWhiteSpace(name))
            {
                // if node has no name, then do not add it.
                installed.Name = name;
                installed.DependentPath = dependentPath;
                if (listObj.ContainsKey("version"))
                {
                    installed.Version = listObj["version"] as string;
                }

                if (listObj.ContainsKey("missing"))
                {
                    installed.IsMissing = true;
                }

                if (listObj.ContainsKey("invalid"))
                {
                    installed.IsOutdated = true;
                }

                if (listObj.ContainsKey("dependencies"))
                {
                    installed.HasDependencies = true;
                }

                parent.Add(installed);
            }

            if (listObj.ContainsKey("dependencies"))
            {
                IDictionary<string, object> dependDict = null;
                object dependencyObj;
                if (listObj.TryGetValue("dependencies", out dependencyObj))
                {
                    dependDict = dependencyObj as IDictionary<string, object>;
                }

                if (dependDict != null && dependDict.Count > 0)
                {
                    string parentName = string.IsNullOrWhiteSpace(name) ? "." : name;
                    string mypath = string.IsNullOrWhiteSpace(dependentPath) ?
                                                        parentName :
                                                        dependentPath + "/" + parentName;
                    foreach (KeyValuePair<string, object> pair in dependDict)
                    {
                        Dictionary<string, object> val = pair.Value as Dictionary<string, object>;
                        if (val != null)
                        {
                            this.InstalledPackageFromDictionary(parent, pair.Key, mypath, val);
                        }
                    }
                }
                else
                {
                    installed.HasDependencies = false;
                }
            }
        }

        /// <summary>
        /// Deserialize the parsed json results to a package for immediate children only
        /// </summary>
        /// <param name="parent">Parent list for this object</param>
        /// <param name="dependentPath">list of parents delimited by "/"</param>
        /// <param name="listObj">dictionary at root of package</param>
        protected void InstalledDependenciesFromDictionary(
                                                    List<INpmInstalledPackage> parent,
                                                    string dependentPath,
                                                    Dictionary<string, object> listObj)
        {
            if (listObj.ContainsKey("dependencies"))
            {
                IDictionary<string, object> dependDict = null;
                object dependencyObj;
                if (listObj.TryGetValue("dependencies", out dependencyObj))
                {
                    dependDict = dependencyObj as IDictionary<string, object>;
                }

                if (dependDict != null && dependDict.Count > 0)
                {
                    foreach (KeyValuePair<string, object> pair in dependDict)
                    {
                        Dictionary<string, object> val = pair.Value as Dictionary<string, object>;
                        if (val != null)
                        {
                            NpmInstalledPackage installed = new NpmInstalledPackage();
                            installed.Name = pair.Key;
                            installed.DependentPath = dependentPath;
                            if (val.ContainsKey("version"))
                            {
                                installed.Version = val["version"] as string;
                            }

                            if (val.ContainsKey("missing"))
                            {
                                installed.IsMissing = true;
                            }

                            if (val.ContainsKey("invalid"))
                            {
                                installed.IsOutdated = true;
                            }

                            if (val.ContainsKey("dependencies"))
                            {
                                installed.HasDependencies = true;
                            }

                            parent.Add(installed);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deserialize the parsed json results and try to match package
        /// </summary>
        /// <param name="package">Package that has name to match</param>
        /// <param name="name">name of package</param>
        /// <param name="dependentPath">list of parents delimited by "/"</param>
        /// <param name="listObj">dictionary at root of package</param>
        /// <returns>Installed package object</returns>
        /// <remarks>This is called recursively as a dependency is a package</remarks>
        protected NpmInstalledPackage MatchPackageFromDictionary(
                                                    INpmPackage package,
                                                    string name,
                                                    string dependentPath,
                                                    Dictionary<string, object> listObj)
        {
            // if name matches and not missing, create package and return it
            if (package.Name == name && !listObj.ContainsKey("missing"))
            {
                NpmInstalledPackage installed = new NpmInstalledPackage();
                installed.Name = name;
                installed.DependentPath = dependentPath;
                if (listObj.ContainsKey("version"))
                {
                    installed.Version = listObj["version"] as string;
                }

                if (listObj.ContainsKey("invalid"))
                {
                    installed.IsOutdated = true;
                }

                if (listObj.ContainsKey("dependencies"))
                {
                    installed.HasDependencies = true;
                }

                return installed;
            }

            // look in the dependencies
            if (listObj.ContainsKey("dependencies"))
            {
                IDictionary<string, object> dependDict = null;
                object dependencyObj;
                if (listObj.TryGetValue("dependencies", out dependencyObj))
                {
                    dependDict = dependencyObj as IDictionary<string, object>;
                }

                if (dependDict != null && dependDict.Count > 0)
                {
                    string mypath = string.IsNullOrWhiteSpace(dependentPath) ?
                                                        name :
                                                        dependentPath + "/" + name;
                    foreach (KeyValuePair<string, object> pair in dependDict)
                    {
                        Dictionary<string, object> val = pair.Value as Dictionary<string, object>;
                        if (val != null)
                        {
                            NpmInstalledPackage installed = this.MatchPackageFromDictionary(package, pair.Key, mypath, val);
                            if (installed != null)
                            {
                                return installed;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
