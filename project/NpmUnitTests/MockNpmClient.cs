using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodejsNpm;

namespace NpmUnitTests
{
    class MockNpmClient : NpmClient
    {
        public MockNpmClient() : base()
        {
        }
        public MockNpmClient(string wd)
            : base(wd)
        {

        }

        public override int Execute(string cmd, string args, out string output, out string err)
        {
            switch (cmd)
            {
                case "--version":
                    output = MockTestData.Version1Text();
                    err = "";
                    return 0;
                case "search":
                    return execSearch(args, out output, out err);
                case "list":
                    return execList(args, out output, out err);
                case "view":
                    return execView(args, out output, out err);
                case "install":
                    return execInstall(args, out output, out err);
                case "outdated":
                    return execOutdated(args, out output, out err);
                case "update":
                    return execUpdate(args, out output, out err);
                case "uninstall":
                    return execUninstall(args, out output, out err);
                default:
                    break;

            }
            output = "";
            err = "";
            return 0;
        }

        private int execSearch(string args, out string output, out string err)
        {
            if (args == "search1")
            {
                output = MockTestData.SearchResult1Text();
                err = "";
                return 0;
            }
            output = "";
            err = "Unknown test arg\n";
            return 1;
        }

        private int execList(string args, out string output, out string err)
        {
            string wd = base.WorkingDirectory;
            if (wd.IndexOf("project1") > 0)
            {
                output = MockTestData.List1Text();
                err = "";
                return 0;
            }
            output = "";
            err = "Unknown working directory\n";
            return 1;
        }

        private int execView(string args, out string output, out string err)
        {
            if (args == "view1")
            {
                output = MockTestData.View1Text();
                err = "";
                return 0;
            }
            output = "";
            err = "Unknown test arg\n";
            return 1;
        }

        private int execInstall(string args, out string output, out string err)
        {
            if (args == "install1")
            {
                output = MockTestData.Install1Text();
                err = "";
                return 0;
            }
            output = "";
            err = "Unknown test arg\n";
            return 1;
        }

        private int execOutdated(string args, out string output, out string err)
        {
            if (args == null)
            {
                output = MockTestData.Outdated1Text();
                err = "";
                return 0;
            }
            else if (args == "outdated1")
            {
                output = MockTestData.Outdated1Text();
                err = "";
                return 0;
            }
            output = "";
            err = "Unknown test arg\n";
            return 1;
        }

        private int execUpdate(string args, out string output, out string err)
        {
            if (args == "update1")
            {
                output = "";
                err = "";
                return 0;
            }
            output = "";
            err = "Unknown test arg\n";
            return 1;
        }

        private int execUninstall(string args, out string output, out string err)
        {
            if (args == "uninstall1")
            {
                output = "";
                err = "";
                return 0;
            }
            output = "";
            err = "Unknown test arg\n";
            return 1;
        }

    }
}
