using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Webmatrix_Npm
{
    internal class NpmSerialize : INpmSerialize
    {
        public IEnumerable<INpmInstalledPackage> FromListInstalled(string jsonlist)
        {
            return null;
        }

        public INpmRemotePackage FromView(string jsonview)
        {
            return null;
        }

        public IEnumerable<INpmInstalledPackage> FromListMissing(string jsonlist)
        {
            return null;
        }

        public IEnumerable<INpmInstalledPackage> FromListOutdated(string jsonlist)
        {
            return null;
        }

        public IEnumerable<INpmPackageDependency> FromOutdatedDependency(string outdated)
        {
            return null;
        }

        /// <summary>
        /// Convert search result text output to collection
        /// </summary>
        /// <param name="output">output text from running npm search</param>
        /// <returns>IEnumerable<INpmSearchResultPackage></returns>
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
                        keywords = keywordlist.Split(new char [] {' '}, StringSplitOptions.RemoveEmptyEntries);
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

        public IEnumerable<INpmPackage> FromInstall(string output)
        {
            return null;
        }
    }
}
