// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft Open Technologies, Inc.">
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
        private const string DefSearchTerm = "azure";

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
            string searchTerm = DefSearchTerm;
            bool runApiSample = true;
            bool runPackageManagerSample = true;
            bool runManualPackageManagerSample = true;

            for (int i = 0; i < args.Count(); i++)
            {
                if (args[i] == "-i")
                {
                    i++;
                    if (i < args.Count())
                    {
                        installPath = Environment.ExpandEnvironmentVariables(args[i]);
                    }
                }
                else if (args[i] == "-w")
                {
                    i++;
                    if (i < args.Count())
                    {
                        wd = Environment.ExpandEnvironmentVariables(args[i]);
                    }
                }
                else if (args[i] == "-a")
                {
                    runApiSample = true;
                    runPackageManagerSample = false;
                    runManualPackageManagerSample = false;
                }
                else if (args[i] == "-p")
                {
                    runApiSample = false;
                    runPackageManagerSample = true;
                    runManualPackageManagerSample = false;
                }
                else if (args[i] == "-m")
                {
                    runApiSample = false;
                    runPackageManagerSample = false;
                    runManualPackageManagerSample = true;
                }
                else if (args[i] == "-?")
                {
                    Console.WriteLine("NpmApiSample [-i <installPath>] [-w <workingdirectory>] [-a | -p] [<modulename>]");
                }
                else
                {
                    searchTerm = args[i];
                }
            }

            if (runApiSample)
            {
                ApiSample.RunSample(wd, installPath, searchTerm);
            }

            if (runPackageManagerSample)
            {
                PackageManagerSample.RunSample(wd, installPath, searchTerm);
            }

            if (runManualPackageManagerSample)
            {
                ManualPmSample.RunSample(wd, installPath, searchTerm);
            }
        }
    }
}
