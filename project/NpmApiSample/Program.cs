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
    using NodeNpm;

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
        /// Default node install path
        /// </summary>
        private const string DefInstallPath = @"%ProgramFiles%\nodejs\";

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            string wd = Environment.CurrentDirectory;
            string installPath = Environment.ExpandEnvironmentVariables(DefInstallPath);
            ApiSample.RunSample(wd, installPath, SearchTerm);
            PackageManagerSample.RunSample(wd, installPath, SearchTerm);
        }
    }
}
