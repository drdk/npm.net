// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft">
// Main class for Npm sample
// </copyright>
// -----------------------------------------------------------------------

namespace NpmApiSample
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using NodejsNpm;

    /// <summary>
    /// Main entry for NpmApiSample program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Term used to search for module to install
        /// </summary>
        private const string SearchTerm = "azure";

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            string wd = Environment.CurrentDirectory;
            ApiSample.RunSample(wd, SearchTerm);
            PackageManagerSample.RunSample(wd, SearchTerm);
        }
    }
}
