using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webmatrix_Npm;

namespace NpmUnitTests
{
    class MockNpmClient : NpmClient
    {
        public MockNpmClient() : base()
        {
        }
        public MockNpmClient(string cwd)
            : base(cwd)
        {
        }

        public override int Execute(string cmd, string args, out string output, out string err)
        {
            switch (cmd)
            {
                case "--version":
                    output = "1.1.9\n\n";
                    err = "";
                    return 0;
                case "search":
                    return execSearch(args, out output, out err);
                default:
                    break;

            }
            output = "";
            err = "";
            return 0;
        }

        private int execSearch(string args, out string output, out string err)
        {
            if (args == "azure")
            {
                output =
                    "NAME                  DESCRIPTION                                                   AUTHOR            DATE              KEYWORDS\n" +
                    "azure                 Windows Azure Client Library for node                         =andrerod         2012-02-16 05:16  node azure\n" +
                    "node-swt              A library to validate and parse swt tokens                    =dario.renzulli   2012-01-18 01:07  swt acs security azure\n" +
                    "node_in_windows_azure An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012 =tomgallacher 2012-01-25 15:19\n";

                err = "";
                return 0;
            }
            output = "";
            err = "Unknown test arg\n";
            return 1;
        }

    }
}
