using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webmatrix_Npm
{
    public class NpmPackage : INpmPackage
    {
        public string Name { get; set; }
        public string Version { get; set; }

        public NpmPackage(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }

    public class NpmSearchResultPackage : INpmSearchResultPackage
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; private set; }
        public string Author { get; private set; }
        public string[] Keywords { get; private set; }
        public DateTime Date { get; private set; }

        public NpmSearchResultPackage(string name, 
                                      string version, 
                                      string description,
                                      string author,
                                      DateTime date,
                                      string[] keywords)
        {
            Name = name;
            Version = version;
            Description = description;
            Author = author;
            Keywords = keywords;
            Date = date;
        }
    }
}
