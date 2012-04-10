namespace NodejsNpm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Script.Serialization;

    /// <summary>
    /// class for serialization from npm to objects
    /// </summary>
    internal class NpmSerialize : INpmSerialize
    {
        /// <summary>
        /// converts npm list output to NpmInstalledPackage
        /// </summary>
        /// <param name="jsonlist">text output</param>
        /// <returns>enumarable NpmInstalledPackage properties</returns>
        public INpmInstalledPackage FromListInstalled(string jsonlist)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            INpmInstalledPackage installed = null;
            Dictionary<string, object> listObj = null;
            try
            {
                listObj = serializer.Deserialize<Dictionary<string, object>>(jsonlist);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Failed to parse output from list command", ex);
            }

            try
            {
                if (listObj != null)
                {
                    object name;
                    listObj.TryGetValue("name", out name);
                    installed = this.InstalledPackageFromDictionary(name as string, listObj);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to convert output from list command to object", ex);
            }

            return installed;
        }

        /// <summary>
        /// convert npm view output to INpmRemotePackage
        /// </summary>
        /// <param name="jsonview">text output</param>
        /// <returns>INpmRemotePackage with property values</returns>
        public INpmRemotePackage FromView(string jsonview)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            INpmRemotePackage view = null;
            Dictionary<string, object> viewObj = null;

            try
            {
                viewObj = serializer.Deserialize<Dictionary<string, object>>(jsonview);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Failed to parse output from view command", ex);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException("Failed to parse output from view command", ex);
            }

            try
            {
                if (viewObj != null)
                {
                    object name;
                    viewObj.TryGetValue("name", out name);
                    view = RemotePackageFromDictionary(name as string, viewObj);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to convert output from view command to object", ex);
            }

            return view;
        }

        /// <summary>
        /// convert missing info from npm list output to NpmInstalledPackage enumeration
        /// </summary>
        /// <param name="jsonlist">text output</param>
        /// <returns>enumarable NpmInstalledPackage properties</returns>
        public IEnumerable<INpmInstalledPackage> FromListMissing(string jsonlist)
        {
            return null;
        }

        /// <summary>
        /// convert outdated info from npm list output to NpmInstalledPackage enumeration
        /// </summary>
        /// <param name="jsonlist">text output</param>
        /// <returns>enumarable NpmInstalledPackage properties</returns>
        public IEnumerable<INpmInstalledPackage> FromListOutdated(string jsonlist)
        {
            return null;
        }

        /// <summary>
        /// convert npm outdated output to NpmPackageDependency enumeration
        /// </summary>
        /// <param name="outdated">text output</param>
        /// <returns>enumarable INpmPackageDependency properties</returns>
        public IEnumerable<INpmPackageDependency> FromOutdatedDependency(string outdated)
        {
            List<INpmPackageDependency> depends = new List<INpmPackageDependency>();
            char[] seps = new char[] { '\n' };
            string[] lines = outdated.Split(seps, StringSplitOptions.RemoveEmptyEntries);

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
        /// <returns>enumarable INpmSearchResultPackage properties</returns>
        public IEnumerable<INpmSearchResultPackage> FromSearchResult(string output)
        {
            List<INpmSearchResultPackage> results = new List<INpmSearchResultPackage>();
            if (output == null)
            {
                return results;
            }

            string[] lines = output.Split('\n');
            int count = lines.Length;
            string pat = @"^(\S+)\s+(.*)=(\S+)\s+([0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2})\s*(.*)$";
            Regex r = new Regex(pat);

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

                Match m = r.Match(lines[ix]);
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
                        DateTime.TryParse(date, out dateParsed);
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
        /// convert npm install output to INpmPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumarable INpmPackage properties</returns>
        public IEnumerable<INpmPackage> FromInstall(string output)
        {
            List<INpmPackage> installs = new List<INpmPackage>();
            char[] seps = new char[] { '\n' };
            string[] lines = output.Split(seps, StringSplitOptions.RemoveEmptyEntries);

            seps = new char[] { ' ' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                int tokenCount = tokens.Length;

                if (tokenCount > 0)
                {
                    NpmPackage package;
                    seps = new char[] { '@' };
                    string[] nameVersion = tokens[0].Split(seps);
                    if (nameVersion.Length > 1)
                    {
                        package = new NpmPackage(nameVersion[0], nameVersion[1]);
                    }
                    else
                    {
                        package = new NpmPackage(nameVersion[0], null);
                    }

                    installs.Add(package);
                }
            }

            return installs;
        }

        /// <summary>
        /// Build a remote package object from the parsed json
        /// </summary>
        /// <param name="name">name of package</param>
        /// <param name="viewObj">dictionary at root of object</param>
        /// <returns>NpmRemotePackage instance</returns>
        private static NpmRemotePackage RemotePackageFromDictionary(string name, Dictionary<string, object> viewObj)
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
                remote.HomepageUrl = viewObj["homepage"] as string;
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
        private static List<string> ConvertStringArray(object obj)
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
        private static NpmPackageDependency ConvertKeyValueToDependency(string key, object value)
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
        private static List<NpmPackageDependency> ConvertDependencies(object obj)
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
        private static NpmReference ConvertReference(object obj)
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
        /// Deserialize the parsed json results to a package
        /// </summary>
        /// <param name="name">name of package</param>
        /// <param name="listObj">dictionary at root of package</param>
        /// <returns>Installed package object</returns>
        /// <remarks>This is called recursively as a dependency is a package</remarks>
        private NpmInstalledPackage InstalledPackageFromDictionary(string name, Dictionary<string, object> listObj)
        {
            NpmInstalledPackage installed = new NpmInstalledPackage();
            installed.Name = name;
            if (listObj.ContainsKey("version"))
            {
                installed.Version = listObj["version"] as string;
            }

            if (listObj.ContainsKey("dependencies"))
            {
                IDictionary<string, object> dependDict = null;
                object dependecyObj;
                if (listObj.TryGetValue("dependencies", out dependecyObj))
                {
                    dependDict = dependecyObj as IDictionary<string, object>;
                }

                if (dependDict != null && dependDict.Count > 0)
                {
                    List<NpmPackageDependency> missingList = null;
                    List<NpmPackageDependency> invalidList = null;
                    List<NpmInstalledPackage> dependencyList = new List<NpmInstalledPackage>(dependDict.Count);
                    foreach (KeyValuePair<string, object> pair in dependDict)
                    {
                        Dictionary<string, object> val = pair.Value as Dictionary<string, object>;
                        if (val != null)
                        {
                            object obj;
                            if (val.TryGetValue("missing", out obj))
                            {
                                if (missingList == null)
                                {
                                    missingList = new List<NpmPackageDependency>();
                                }

                                NpmPackageDependency dependency = ConvertKeyValueToDependency(pair.Key, obj);
                                missingList.Add(dependency);
                                continue;
                            }

                            if (val.TryGetValue("invalid", out obj))
                            {
                                if (invalidList == null)
                                {
                                    invalidList = new List<NpmPackageDependency>();
                                }

                                NpmPackageDependency dependency = ConvertKeyValueToDependency(pair.Key, obj);
                                invalidList.Add(dependency);
                                continue;
                            }

                            NpmInstalledPackage installedDepends = this.InstalledPackageFromDictionary(pair.Key, val);
                            if (installedDepends != null)
                            {
                                dependencyList.Add(installedDepends);
                            }
                        }
                    }

                    installed.InstalledDependencies = dependencyList;
                    if (missingList != null)
                    {
                        installed.MissingDependencies = missingList;
                    }

                    if (invalidList != null)
                    {
                        installed.OutdatedDependencies = invalidList;
                    }
                }
            }

            return installed;
        }
    }
}
