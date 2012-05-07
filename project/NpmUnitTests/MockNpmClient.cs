// -----------------------------------------------------------------------
// <copyright file="MockNpmClient.cs" company="Microsoft">
// Class for npm package manager unit tests
// </copyright>
// -----------------------------------------------------------------------

namespace NpmUnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NodeNpm;

    /// <summary>
    /// The class that emulates NpmClient and provides predictable responses to commands
    /// </summary>
    internal class MockNpmClient : INpmClient
    {
        /// <summary>
        /// State used to alter output after uninstall
        /// </summary>
        private bool calledUninstall;

        /// <summary>
        /// The output from most recent execute
        /// </summary>
        private string lastExecuteOutput;

        /// <summary>
        /// The error output from most recent execute
        /// </summary>
        private string lastExecuteErrorText;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockNpmClient" /> class.
        /// </summary>
        public MockNpmClient()
        {
            this.calledUninstall = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockNpmClient" /> class.
        /// </summary>
        /// <param name="wd">Working directory</param>
        public MockNpmClient(string wd)
        {
            this.WorkingDirectory = wd;
            this.calledUninstall = false;
        }

        /// <summary>
        /// Gets or sets installation path for node
        /// </summary>
        public string InstallPath { get; set; }

        /// <summary>
        /// Gets or sets relative path to NPM from node installation
        /// </summary>
        public string NpmRelativePath { get; set; }

        /// <summary>
        /// Gets or sets project directory used for some NPM commands
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets URI of remote registry. Only set if not using default NPM.
        /// </summary>
        public Uri Registry { get; set; }

        /// <summary>
        /// Gets or sets HTTP Proxy URL
        /// </summary>
        public string HttpProxy { get; set; }

        /// <summary>
        /// Gets or sets HTTPS Proxy URL
        /// </summary>
        public string HttpsProxy { get; set; }

        /// <summary>
        /// Gets or sets timeout to use for NPM commands
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets the output text from the last NPM command
        /// </summary>
        public string LastExecuteOutput
        {
            get
            {
                return this.lastExecuteOutput;
            }
        }

        /// <summary>
        /// Gets the error text from the last NPM command
        /// </summary>
        public string LastExecuteErrorText
        {
            get
            {
                return this.lastExecuteErrorText;
            }
        }

        /// <summary>
        /// Execute NPM command and save output
        /// </summary>
        /// <param name="cmd">command name</param>
        /// <param name="args">remainder of npm command line</param>
        /// <returns>exit code. 0 is success</returns>
        /// <remarks>LastExecuteOutput and LastExecuteError will be set</remarks>
        public int Execute(string cmd, string args)
        {
            this.lastExecuteErrorText = string.Empty;
            this.lastExecuteOutput = string.Empty;
            switch (cmd)
            {
                case "--version":
                    this.lastExecuteOutput = MockTestData.Version1Text();
                    return 0;
                case "search":
                    return this.ExecuteSearch(args);
                case "list":
                    return this.ExecuteList(args);
                case "view":
                    return this.ExecuteView(args);
                case "install":
                    return this.ExecuteInstall(args);
                case "outdated":
                    return this.ExecuteOutdated(args);
                case "update":
                    return this.ExecuteUpdate(args);
                case "uninstall":
                    return this.ExecuteUninstall(args);
                default:
                    break;
            }

            this.lastExecuteErrorText = "Unknown command\n";
            return 0;
        }

        /// <summary>
        /// Mock execution of search command
        /// </summary>
        /// <param name="args">Arguments for search command</param>
        /// <returns>return code</returns>
        private int ExecuteSearch(string args)
        {
            if (args.StartsWith("/.*", StringComparison.InvariantCultureIgnoreCase))
            {
                this.lastExecuteOutput = MockTestData.SearchResult2Text();
                return 0;
            }
            else if (args.StartsWith("search1", StringComparison.InvariantCultureIgnoreCase))
            {
                this.lastExecuteOutput = MockTestData.SearchResult1Text();
                return 0;
            }

            this.lastExecuteErrorText = "Unknown test arg\n";
            return 1;
        }

        /// <summary>
        /// Mock execution of list command
        /// </summary>
        /// <param name="args">Arguments for list command</param>
        /// <returns>return code</returns>
        private int ExecuteList(string args)
        {
            string wd = this.WorkingDirectory;
            if (wd.IndexOf("project1") > 0)
            {
                this.lastExecuteOutput = MockTestData.List1Text();
                return 0;
            }
            if (wd.IndexOf("project2") > 0)
            {
                this.lastExecuteOutput = MockTestData.List2Text();
                return 0;
            }
            else if (wd.IndexOf("uninstall1") > 0)
            {
                if (this.calledUninstall)
                {
                    this.calledUninstall = false;
                    this.lastExecuteOutput = MockTestData.ListAfterUninstallText();
                    return 0;
                }
                else
                {
                    this.lastExecuteOutput = MockTestData.ListBeforeUninstallText();
                    return 0;
                }
            }
            else if (wd.IndexOf("outdatedmulti") > 0)
            {
                this.lastExecuteOutput = MockTestData.ListOutdatedMultiText();
                return 0;
            }
            else if (wd.IndexOf("update1") > 0)
            {
                this.lastExecuteOutput = MockTestData.ListProblems1Text();
                return 0;
            }
            else if (wd.IndexOf("empty1") > 0)
            {
                this.lastExecuteOutput = MockTestData.ListEmptyText();
                return 0;
            }

            this.lastExecuteErrorText = "Unknown working directory\n";
            return 1;
        }

        /// <summary>
        /// Mock execution of view command
        /// </summary>
        /// <param name="args">Arguments for view command</param>
        /// <returns>return code</returns>
        private int ExecuteView(string args)
        {
            if (args.StartsWith("view1", StringComparison.InvariantCultureIgnoreCase))
            {
                this.lastExecuteOutput = MockTestData.View1Text();
                return 0;
            }

            this.lastExecuteErrorText = "Unknown test arg\n";
            return 1;
        }

        /// <summary>
        /// Mock execution of install command
        /// </summary>
        /// <param name="args">Arguments for install command</param>
        /// <returns>return code</returns>
        private int ExecuteInstall(string args)
        {
            if (args.StartsWith("underscore", StringComparison.InvariantCultureIgnoreCase))
            {
                this.lastExecuteOutput = MockTestData.Install1Text();
                return 0;
            }
            else if (args.StartsWith("install1", StringComparison.InvariantCultureIgnoreCase))
            {
                this.lastExecuteOutput = MockTestData.Install1Text();
                return 0;
            }
            else if (args.StartsWith("bogusmod", StringComparison.InvariantCultureIgnoreCase))
            {
                this.lastExecuteErrorText = MockTestData.ErrorInstallText();
                return 1;
            }

            this.lastExecuteErrorText = "Unknown test arg\n";
            return 1;
        }

        /// <summary>
        /// Mock execution of outdated command
        /// </summary>
        /// <param name="args">Arguments for outdated command</param>
        /// <returns>return code</returns>
        private int ExecuteOutdated(string args)
        {
            string wd = this.WorkingDirectory;
            if (wd.IndexOf("outdatedmulti") > 0)
            {
                if (args == null)
                {
                    this.lastExecuteOutput = MockTestData.OutdatedChildText();
                    return 0;
                }
                else if (args.StartsWith("outdatedparent", StringComparison.InvariantCultureIgnoreCase))
                {
                    this.lastExecuteOutput = MockTestData.OutdatedParentText();
                    return 0;
                }
                else if (args.StartsWith("outdated1", StringComparison.InvariantCultureIgnoreCase))
                {
                    this.lastExecuteOutput = MockTestData.Outdated1Text();
                    return 0;
                }
            }
            else
            {
                if (args == null)
                {
                    this.lastExecuteOutput = MockTestData.Outdated1Text();
                    return 0;
                }
                else if (args.IndexOf("underscore") > 0)
                {
                    this.lastExecuteOutput = MockTestData.Outdated1Text();
                    return 0;
                }
                else if (args.StartsWith("outdated1", StringComparison.InvariantCultureIgnoreCase))
                {
                    this.lastExecuteOutput = MockTestData.Outdated1Text();
                    return 0;
                }
            }

            this.lastExecuteErrorText = "Unknown test arg\n";
            return 1;
        }

        /// <summary>
        /// Mock execution of update command
        /// </summary>
        /// <param name="args">Arguments for update command</param>
        /// <returns>return code</returns>
        private int ExecuteUpdate(string args)
        {
            if (args.StartsWith("underscore", StringComparison.InvariantCultureIgnoreCase))
            {
                this.lastExecuteOutput = MockTestData.Install1Text();
                return 0;
            }

            this.lastExecuteErrorText = "Unknown test arg\n";
            return 1;
        }

        /// <summary>
        /// Mock execution of uninstall command
        /// </summary>
        /// <param name="args">Arguments for uninstall command</param>
        /// <returns>return code</returns>
        private int ExecuteUninstall(string args)
        {
            if (args.StartsWith("uninstall1", StringComparison.InvariantCultureIgnoreCase))
            {
                this.calledUninstall = true;
                this.lastExecuteOutput = string.Empty;
                return 0;
            }

            this.lastExecuteErrorText = "Unknown test arg\n";
            return 1;
        }
    }
}
