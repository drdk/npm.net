namespace Webmatrix_Npm
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// class for serialization from npm to objects
    /// </summary>
    internal class NpmSerialize : INpmSerialize
    {
        /// <summary>
        /// converts npm list output to NpmInstalledPackage enumeration
        /// </summary>
        /// <param name="jsonlist">text output</param>
        /// <returns>enumarable NpmInstalledPackage properties</returns>
        public IEnumerable<INpmInstalledPackage> FromListInstalled(string jsonlist)
        {
            return null;
        }

        /// <summary>
        /// convert npm view output to INpmRemotePackage
        /// </summary>
        /// <param name="jsonview">text output</param>
        /// <returns>INpmRemotePackage with property values</returns>
        public INpmRemotePackage FromView(string jsonview)
        {
            return null;
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
            return null;
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
            return null;
        }
    }
}
