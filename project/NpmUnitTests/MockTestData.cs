// -----------------------------------------------------------------------
// <copyright file="MockTestData.cs" company="Microsoft">
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
    /// Data used as input or as expected result for unit tests
    /// </summary>
    internal static class MockTestData
    {
        /// <summary>
        /// Npm text output for install - old style (pre 1.1.16)
        /// </summary>
        /// <returns>Predictable output text for install test</returns>
        public static string InstallOld1Text()
        {
            return
                "{\n" +
                "  \"./node_modules/underscore\": {\n" +
                "    \"parent\": null,\n" +
                "    \"children\": [],\n" +
                "    \"where\": \"./node_modules/underscore\",\n" +
                "    \"what\": \"underscore@1.3.3\",\n" +
                "    \"from\": \"underscore\"\n" +
                "  }\n" +
                "}\n";
        }

        /// <summary>
        /// Npm text output for install
        /// </summary>
        /// <returns>Predictable output text for install test</returns>
        public static string Install1Text()
        {
            return
                "[\n" +
                "  {\n" +
                "    \"name\": \"underscore\",\n" +
                "    \"version\": \"1.3.3\",\n" +
                "    \"from\": \"underscore\",\n" +
                "    \"dependencies\": {}\n" +
                "  }\n" +
                "]\n";
        }

        /// <summary>
        /// Expected output from install
        /// </summary>
        /// <returns>Expected result for install test</returns>
        public static List<NpmInstalledPackage> Install1Expected()
        {
            List<NpmInstalledPackage> expected = new List<NpmInstalledPackage>();
            NpmInstalledPackage package = new NpmInstalledPackage();
            package.Name = "underscore";
            package.Version = "1.3.3";
            package.DependentPath = string.Empty;
            expected.Add(package);
            return expected;
        }

        /// <summary>
        /// Expected output from install
        /// </summary>
        /// <returns>Expected result for install test - high level</returns>
        public static List<string> Install1ExpectedNames()
        {
            List<string> expected = new List<string>();
            expected.Add("underscore");
            return expected;
        }

        /// <summary>
        /// Expected input for list
        /// </summary>
        /// <returns>Predictable output text for list test</returns>
        public static string List1Text()
        {
            return
                    "{\n" +
                    "  \"name\": \"azure\",\n" +
                    "  \"version\": \"0.5.2\",\n" +
                    "  \"dependencies\": {\n" +
                    "    \"xml2js\": {\n" +
                    "      \"version\": \"0.1.13\"\n" +
                    "    },\n" +
                    "    \"sax\": {\n" +
                    "      \"version\": \"0.4.0\"\n" +
                    "    },\n" +
                    "    \"jshint\": {\n" +
                    "      \"version\": \"0.5.9\",\n" +
                    "      \"dependencies\": {\n" +
                    "        \"argsparser\": {\n" +
                    "          \"version\": \"0.0.6\"\n" +
                    "        }\n" +
                    "      }\n" +
                    "    }\n" +
                    "  }\n" +
                    "}";
        }

        /// <summary>
        /// Expected input for list
        /// </summary>
        /// <returns>Predictable output text for list test</returns>
        /// <remarks>root directory not a package</remarks>
        public static string List2Text()
        {
            return
                    "{\n" +
                    "  \"dependencies\": {\n" +
                    "    \"azure\": {\n" +
                    "      \"version\": \"0.5.2\",\n" +
                    "      \"dependencies\": {\n" +
                    "        \"xml2js\": {\n" +
                    "          \"version\": \"0.1.13\"\n" +
                    "        },\n" +
                    "        \"sax\": {\n" +
                    "          \"version\": \"0.4.0\"\n" +
                    "        },\n" +
                    "        \"jshint\": {\n" +
                    "          \"version\": \"0.5.9\",\n" +
                    "          \"dependencies\": {\n" +
                    "            \"argsparser\": {\n" +
                    "              \"version\": \"0.0.6\"\n" +
                    "            }\n" +
                    "          }\n" +
                    "        }\n" +
                    "      }\n" +
                    "    }\n" +
                    "  }\n" +
                    "}";
        }

        /// <summary>
        /// Expected output from list
        /// </summary>
        /// <returns>Expected result for list test</returns>
        public static List<NpmInstalledPackage> List1Expected()
        {
            List<NpmInstalledPackage> expected = new List<NpmInstalledPackage>();
            NpmInstalledPackage package;
            package = new NpmInstalledPackage();
            package.Name = "azure";
            package.Version = "0.5.2";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = true;
            package.DependentPath = string.Empty;
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "xml2js";
            package.Version = "0.1.13";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "sax";
            package.Version = "0.4.0";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "jshint";
            package.Version = "0.5.9";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = true;
            package.DependentPath = "azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "argsparser";
            package.Version = "0.0.6";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "azure/jshint";
            expected.Add(package);
            return expected;
        }

        /// <summary>
        /// Expected output from list
        /// </summary>
        /// <returns>Expected result for list test</returns>
        public static List<NpmInstalledPackage> List2Expected()
        {
            List<NpmInstalledPackage> expected = new List<NpmInstalledPackage>();
            NpmInstalledPackage package;
            package = new NpmInstalledPackage();
            package.Name = "azure";
            package.Version = "0.5.2";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = true;
            package.DependentPath = ".";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "xml2js";
            package.Version = "0.1.13";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "./azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "sax";
            package.Version = "0.4.0";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "./azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "jshint";
            package.Version = "0.5.9";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = true;
            package.DependentPath = "./azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "argsparser";
            package.Version = "0.0.6";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "./azure/jshint";
            expected.Add(package);
            return expected;
        }

        /// <summary>
        /// Expected output from list
        /// </summary>
        /// <returns>Expected result for list test</returns>
        public static List<NpmInstalledPackage> List1ChildrenExpected()
        {
            List<NpmInstalledPackage> expected = new List<NpmInstalledPackage>();
            NpmInstalledPackage package;
            package = new NpmInstalledPackage();
            package.Name = "xml2js";
            package.Version = "0.1.13";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "sax";
            package.Version = "0.4.0";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = false;
            package.DependentPath = "azure";
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "jshint";
            package.Version = "0.5.9";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = true;
            package.DependentPath = "azure";
            expected.Add(package);
            return expected;
        }

        /// <summary>
        /// Expected output from list
        /// </summary>
        /// <returns>Expected result for list test</returns>
        public static List<NpmInstalledPackage> List2ChildrenExpected()
        {
            List<NpmInstalledPackage> expected = new List<NpmInstalledPackage>();
            NpmInstalledPackage package;
            package = new NpmInstalledPackage();
            package.Name = "azure";
            package.Version = "0.5.2";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.HasDependencies = true;
            package.DependentPath = ".";
            expected.Add(package);
            return expected;
        }

        /// <summary>
        /// Input for IsInstalled test
        /// </summary>
        /// <returns>Input data for IsInstalled test</returns>
        public static NpmPackage List1MatchInstalledPackage()
        {
            NpmPackage package;
            package = new NpmPackage("xml2js", null);
            return package;
        }

        /// <summary>
        /// Expected result for IsInstalled
        /// </summary>
        /// <returns>Expected result for IsInstalled test</returns>
        public static NpmInstalledPackage List1MatchInstalledExpected()
        {
            NpmInstalledPackage package;
            package = new NpmInstalledPackage();
            package.Name = "xml2js";
            package.Version = "0.1.13";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.DependentPath = "azure";
            return package;
        }

        /// <summary>
        /// Input for IsInstalled
        /// </summary>
        /// <returns>Input for IsInstalled test</returns>
        public static NpmPackage List2MatchInstalledPackage()
        {
            NpmPackage package;
            package = new NpmPackage("bogus", null);
            return package;
        }

        /// <summary>
        /// Expected result for IsInstalled
        /// </summary>
        /// <returns>Expected result for IsInstalled test</returns>
        public static NpmInstalledPackage List2MatchInstalledExpected()
        {
            return null;
        }

        /// <summary>
        /// Expected input for list with issues
        /// </summary>
        /// <returns>Predictable output text for list test</returns>
        public static string ListProblems1Text()
        {
            return
                    "{\n" +
                    "  \"name\": \"azure\",\n" +
                    "  \"version\": \"0.5.2\",\n" +
                    "  \"dependencies\": {\n" +
                    "    \"xml2js\": {\n" +
                    "      \"version\": \"0.1.13\"\n" +
                    "    },\n" +
                    "    \"mime\": {\n" +
                    "      \"required\": \">= 1.2.4\",\n" +
                    "      \"missing\": true\n" +
                    "    },\n" +
                    "    \"underscore\": {\n" +
                    "      \"version\": \"1.3.0\",\n" +
                    "      \"invalid\": true,\n" +
                    "      \"problems\": [\n" +
                    "        \"invalid: underscore@1.3.0 C:\\\\azure-pr\\\\azure-sdk-for-node-pr\\\\node_modules\\\\underscore\"\n" +
                    "      ]\n" +
                    "    }\n" +
                    "  }\n" +
                    "}";
        }

        /// <summary>
        /// Expected output from list
        /// </summary>
        /// <returns>Expected result for list test</returns>
        public static List<NpmInstalledPackage> ListProblem1Expected()
        {
            List<NpmInstalledPackage> expected = new List<NpmInstalledPackage>();
            NpmInstalledPackage package;
            package = new NpmInstalledPackage();
            package.Name = "azure";
            package.Version = "0.5.2";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.DependentPath = string.Empty;
            package.HasDependencies = true;
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "xml2js";
            package.Version = "0.1.13";
            package.IsMissing = false;
            package.IsOutdated = false;
            package.DependentPath = "azure";
            package.HasDependencies = false;
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "mime";
            package.IsMissing = true;
            package.IsOutdated = false;
            package.DependentPath = "azure";
            package.HasDependencies = false;
            expected.Add(package);
            package = new NpmInstalledPackage();
            package.Name = "underscore";
            package.Version = "1.3.0";
            package.IsMissing = false;
            package.IsOutdated = true;
            package.DependentPath = "azure";
            package.HasDependencies = false;
            expected.Add(package);
            return expected;
        }

        /// <summary>
        /// Expected input for outdated
        /// </summary>
        /// <returns>Predictable output text for outdated test</returns>
        public static string Outdated1Text()
        {
            return "underscore@1.3.1 ./node_modules/underscore current=1.3.0\n";
        }

        /// <summary>
        /// Expected output from outdated with name
        /// </summary>
        /// <returns>Expected result for outdated test</returns>
        public static NpmPackageDependency OutdatedSingle1Expected()
        {
            NpmPackageDependency dependency = new NpmPackageDependency();
            dependency.Name = "underscore";
            dependency.Version = "1.3.0";
            dependency.VersionRange = "1.3.1";
            return dependency;
        }

        /// <summary>
        /// Expected output from outdated
        /// </summary>
        /// <returns>Expected result for outdated test</returns>
        public static List<NpmPackageDependency> Outdated1Expected()
        {
            List<NpmPackageDependency> expected = new List<NpmPackageDependency>();
            NpmPackageDependency dependency = new NpmPackageDependency();
            dependency.Name = "underscore";
            dependency.Version = "1.3.0";
            dependency.VersionRange = "1.3.1";
            expected.Add(dependency);
            return expected;
        }

        /// <summary>
        /// Expected input for search
        /// </summary>
        /// <returns>Predictable output text for search test</returns>
        public static string SearchResult1Text()
        {
            return
                "NAME                  DESCRIPTION                                                   AUTHOR            DATE              KEYWORDS\n" +
                "azure                 Windows Azure Client Library for node                         =andrerod         2012-02-16 05:16  node azure\n" +
                "node-swt              A library to validate and parse swt tokens                    =dario.renzulli   2012-01-18 01:07  swt acs security azure\n" +
                "node_in_windows_azure An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012 =tomgallacher 2012-01-25 15:19\n";
        }

        /// <summary>
        /// Expected output from search
        /// </summary>
        /// <returns>Expected result for search test</returns>
        public static List<NpmSearchResultPackage> SearchResult1Expected()
        {
            List<NpmSearchResultPackage> expected = new List<NpmSearchResultPackage>();
            NpmSearchResultPackage res = new NpmSearchResultPackage(
                "azure",
                null,
                "Windows Azure Client Library for node",
                "andrerod",
                new DateTime(2012, 2, 16, 5, 16, 0),
                new string[] { "node", "azure" });
            NpmSearchResultPackage res2 = new NpmSearchResultPackage(
                "node-swt",
                null,
                "A library to validate and parse swt tokens",
                "dario.renzulli",
                new DateTime(2012, 1, 18, 1, 7, 0),
                new string[] { "swt", "acs", "security", "azure" });
            NpmSearchResultPackage res3 = new NpmSearchResultPackage(
                "node_in_windows_azure",
                null,
                "An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012",
                "tomgallacher",
                new DateTime(2012, 1, 25, 15, 19, 0),
                new string[] { });
            expected.Add(res);
            expected.Add(res2);
            expected.Add(res3);
            return expected;
        }

        /// <summary>
        /// Expected input for search
        /// </summary>
        /// <returns>Predictable output text for search test</returns>
        public static string SearchResult2Text()
        {
            return
                "NAME                  DESCRIPTION                                                   AUTHOR            DATE              KEYWORDS\n" +
                "azure                 Windows Azure Client Library for node                         =andrerod         2012-02-16 05:16  node azure\n" +
                "node-swt              A library to validate and parse swt tokens                    =dario.renzulli   2012-01-18 01:07  swt acs security azure\n" +
                "node_in_windows_azure An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012 =tomgallacher 2012-01-25 15:19\n" +
                "video             A C++ module for node.js that creates Theora/Ogg videos from RGB frames. =pkrumins 2012-04-01 17:09  video videos theora rgb\n";
        }

        /// <summary>
        /// Expected output from search
        /// </summary>
        /// <returns>Expected result for search test</returns>
        public static List<NpmSearchResultPackage> SearchResult2Expected()
        {
            List<NpmSearchResultPackage> expected = new List<NpmSearchResultPackage>();
            NpmSearchResultPackage res = new NpmSearchResultPackage(
                "azure",
                null,
                "Windows Azure Client Library for node",
                "andrerod",
                new DateTime(2012, 2, 16, 5, 16, 0),
                new string[] { "node", "azure" });
            NpmSearchResultPackage res2 = new NpmSearchResultPackage(
                "node-swt",
                null,
                "A library to validate and parse swt tokens",
                "dario.renzulli",
                new DateTime(2012, 1, 18, 1, 7, 0),
                new string[] { "swt", "acs", "security", "azure" });
            NpmSearchResultPackage res3 = new NpmSearchResultPackage(
                "node_in_windows_azure",
                null,
                "An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012",
                "tomgallacher",
                new DateTime(2012, 1, 25, 15, 19, 0),
                new string[] { });
            NpmSearchResultPackage res4 = new NpmSearchResultPackage(
                "video",
                null,
                "A C++ module for node.js that creates Theora/Ogg videos from RGB frames.",
                "pkrumins",
                new DateTime(2012, 4, 1, 17, 9, 0),
                new string[] { "video", "videos", "theora", "rgb" });
            expected.Add(res);
            expected.Add(res2);
            expected.Add(res3);
            expected.Add(res4);
            return expected;
        }

        /// <summary>
        /// Expected input for view
        /// </summary>
        /// <returns>Predictable output text for view test</returns>
        public static string View1Text()
        {
            return
                    "{ name: 'dateformat',\n" +
                    "  description: 'A node.js package for Steven Levithan\\'s excellent dateFormat()function.',\n" +
                    "  'dist-tags': { latest: '1.0.2-1.2.3' },\n" +
                    "  versions:\n" +
                    "   [ '0.9.0-1.2.3',\n" +
                    "     '1.0.0-1.2.3',\n" +
                    "     '1.0.1-1.2.3',\n" +
                    "     '1.0.2-1.2.3' ],\n" +
                    "  maintainers: 'felixge <felix@debuggable.com>',\n" +
                    "  time:\n" +
                    "   { '0.9.0-1.2.3': '2011-03-13T16:29:39.454Z',\n" +
                    "     '1.0.0-1.2.3': '2011-03-13T16:32:25.648Z',\n" +
                    "     '1.0.1-1.2.3': '2011-04-25T15:30:05.199Z',\n" +
                    "     '1.0.2-1.2.3': '2011-09-28T11:06:44.207Z' },\n" +
                    "  author: 'Steven Levithan',\n" +
                    "  homepage: 'https://github.com/felixge/node-dateformat',\n" +
                    "  repository:\n" +
                    "   { type: 'git',\n" +
                    "     url: 'git://github.com/felixge/dateformat.git' },\n" +
                    "  version: '1.0.2-1.2.3',\n" +
                    "  main: './lib/dateformat',\n" +
                    "  dependencies: {},\n" +
                    "  devDependencies: {},\n" +
                    "  engines: { node: '*' },\n" +
                    "  dist:\n" +
                    "   { shasum: 'b0220c02de98617433b72851cf47de3df2cdbee9',\n" +
                    "     tarball: 'http://registry.npmjs.org/dateformat/-/dateformat-1.0.2-1.2.3.tgz' },\n" +
                    "  scripts: {},\n" +
                    "  directories: {},\n" +
                    "  optionalDependencies: {} }";
        }

        /// <summary>
        /// Expected output from view
        /// </summary>
        /// <returns>Expected result for view test</returns>
        public static NpmRemotePackage View1Expected()
        {
            NpmRemotePackage expected = new NpmRemotePackage();
            expected.Name = "dateformat";
            expected.Version = "1.0.2-1.2.3";
            expected.Description = "A node.js package for Steven Levithan's excellent dateFormat()function.";
            List<string> versions = new List<string>();
            versions.Add("0.9.0-1.2.3");
            versions.Add("1.0.0-1.2.3");
            versions.Add("1.0.1-1.2.3");
            versions.Add("1.0.2-1.2.3");
            expected.Versions = versions;
            List<string> maintainers = new List<string>();
            maintainers.Add("felixge <felix@debuggable.com>");
            expected.Maintainers = maintainers;
            expected.Contributors = null;
            expected.Homepage = "https://github.com/felixge/node-dateformat";
            expected.Author = "Steven Levithan";
            expected.Dependencies = new List<NpmPackageDependency>();
            expected.DevDependencies = new List<NpmPackageDependency>();
            expected.OptionalDependencies = null;
            expected.License = null;
            expected.Repository = null;
            return expected;
        }

        /// <summary>
        /// Expected input for version
        /// </summary>
        /// <returns>Predictable output text for version test</returns>
        public static string Version1Text()
        {
            return "1.1.20\n\n";
        }

        /// <summary>
        /// Expected output from version
        /// </summary>
        /// <returns>Expected result for version test</returns>
        public static string Version1Expected()
        {
            return "1.1.20";
        }

        /// <summary>
        /// Expected input for list
        /// </summary>
        /// <returns>Predictable output text for list test</returns>
        public static string ListBeforeUninstallText()
        {
            return
                    "{\n" +
                    "  \"dependencies\": {\n" +
                    "    \"xml2js\": {\n" +
                    "      \"version\": \"0.1.13\"\n" +
                    "    },\n" +
                    "    \"uninstall1\": {\n" +
                    "      \"version\": \"0.4.0\"\n" +
                    "    },\n" +
                    "    \"jshint\": {\n" +
                    "      \"version\": \"0.5.9\",\n" +
                    "      \"dependencies\": {\n" +
                    "        \"argsparser\": {\n" +
                    "          \"version\": \"0.0.6\"\n" +
                    "        }\n" +
                    "      }\n" +
                    "    }\n" +
                    "  }\n" +
                    "}";
        }

        /// <summary>
        /// Expected input for list
        /// </summary>
        /// <returns>Predictable output text for list test</returns>
        public static string ListAfterUninstallText()
        {
            return
                    "{\n" +
                    "  \"dependencies\": {\n" +
                    "    \"xml2js\": {\n" +
                    "      \"version\": \"0.1.13\"\n" +
                    "    },\n" +
                    "    \"jshint\": {\n" +
                    "      \"version\": \"0.5.9\",\n" +
                    "      \"dependencies\": {\n" +
                    "        \"argsparser\": {\n" +
                    "          \"version\": \"0.0.6\"\n" +
                    "        }\n" +
                    "      }\n" +
                    "    }\n" +
                    "  }\n" +
                    "}";
        }

        /// <summary>
        /// Expected output from uninstall
        /// </summary>
        /// <returns>Expected result for uninstall test</returns>
        public static List<string> Uninstall1Expected()
        {
            List<string> uninstallList = new List<string>();
            uninstallList.Add("uninstall1");
            return uninstallList;
        }

        /// <summary>
        /// Expected input for outdated module with outdated dependecy
        /// </summary>
        /// <returns>Predictable output text for outdated test</returns>
        public static string ListOutdatedMultiText()
        {
            return
                    "{\n" +
                    "  \"name\": \"root\",\n" +
                    "  \"version\": \"0.5.2\",\n" +
                    "  \"dependencies\": {\n" +
                    "    \"outdatedparent\": {\n" +
                    "      \"version\": \"0.5.2\",\n" +
                    "      \"dependencies\": {\n" +
                    "        \"current\": {\n" +
                    "          \"version\": \"0.1.13\"\n" +
                    "        },\n" +
                    "        \"outdatedchild\": {\n" +
                    "          \"version\": \"1.3.0\",\n" +
                    "          \"invalid\": true,\n" +
                    "          \"problems\": [\n" +
                    "            \"invalid: outdatedchild@1.3.0 C:\\\\mock\\\\mockdata\\\\node_modules\\\\outdatedchild\"\n" +
                    "          ]\n" +
                    "        }\n" +
                    "      }\n" +
                    "    }\n" +
                    "  }\n" +
                    "}";
        }

        /// <summary>
        /// Expected input for outdated
        /// </summary>
        /// <returns>Predictable output text for outdated test</returns>
        public static string OutdatedParentText()
        {
            return "outdatedparent@0.5.4 ./node_modules/outdatedparent current=0.5.2\n";
        }

        /// <summary>
        /// Expected input for outdated
        /// </summary>
        /// <returns>Predictable output text for outdated test</returns>
        public static string OutdatedChildText()
        {
            return "outdatedchild@1.3.1 ./node_modules/outdatedparent//node_modules/outdatedchild current=1.3.0\n";
        }

        /// <summary>
        /// Expected output from outdated
        /// </summary>
        /// <returns>Expected result for outdated test</returns>
        public static List<NpmPackageDependency> OutdatedMultiExpected()
        {
            List<NpmPackageDependency> expected = new List<NpmPackageDependency>();
            NpmPackageDependency dependency = new NpmPackageDependency();
            dependency.Name = "outdatedparent";
            dependency.Version = "0.5.2";
            dependency.VersionRange = "0.5.4";
            expected.Add(dependency);
            dependency = new NpmPackageDependency();
            dependency.Name = "outdatedchild";
            dependency.Version = "1.3.0";
            dependency.VersionRange = "1.3.1";
            expected.Add(dependency);
            return expected;
        }

        /// <summary>
        /// Expected input for list
        /// </summary>
        /// <returns>Predictable output text for list test</returns>
        public static string ListEmptyText()
        {
            return "{}\n";
        }

        /// <summary>
        /// Expected output from list
        /// </summary>
        /// <returns>Expected result for list test</returns>
        public static List<NpmInstalledPackage> ListEmptyExpected()
        {
            List<NpmInstalledPackage> expected = new List<NpmInstalledPackage>();
            return expected;
        }

        /// <summary>
        /// Expected input for install error
        /// </summary>
        /// <returns>Predictable output text for install error</returns>
        public static string ErrorInstallText()
        {
            return
                "npm http GET https://registry.npmjs.org/fffggghhh\n" +
                "npm http 404 https://registry.npmjs.org/fffggghhh\n" +
                "\n" +
                "npm ERR! 404 'bogusmod' is not in the npm registry.\n" +
                "npm ERR! 404 You should bug the author to publish it\n" +
                "npm ERR! 404 \n" +
                "npm ERR! 404 Note that you can also install from a\n" +
                "npm ERR! 404 tarball, folder, or http url, or git url.\n" +
                "npm ERR! \n" +
                "npm ERR! System Windows_NT 6.1.7601\n" +
                "npm ERR! command \"C:\\Program Files (x86)\\nodejs\\node.exe\" \"C:\\Program Files (x86)\\nodejs\\node_modules\\npm\\bin\\npm-cli.js\" \"install\" \"fffggghhh\"\n" +
                "npm ERR! cwd C:\\src\n" +
                "npm ERR! node -v v0.6.13\n" +
                "npm ERR! npm -v 1.1.9\n" +
                "npm ERR! code E404\n" +
                "npm ERR! message 404 Not Found: bogusmod\n" +
                "npm ERR! errno {}\n" +
                "npm ERR! \n" +
                "npm ERR! Additional logging details can be found in:\n" +
                "npm ERR!     C:\\src\\npm-debug.log\n" +
                "npm not ok";
        }

        /// <summary>
        /// Expected exception for install error
        /// </summary>
        /// <returns>Predictable exception for install error</returns>
        public static NpmException ErrorInstallExpected()
        {
            NpmException except = new NpmException("Failed: npm reported an error.");
            except.NpmSystem = "Windows_NT 6.1.7601";
            except.NpmCommand = "\"C:\\Program Files (x86)\\nodejs\\node.exe\" \"C:\\Program Files (x86)\\nodejs\\node_modules\\npm\\bin\\npm-cli.js\" \"install\" \"fffggghhh\"";
            except.NpmCwd = "C:\\src";
            except.NpmNodeVersion = "v0.6.13";
            except.NpmNpmVersion = "1.1.9";
            except.NpmCode = "E404";
            except.NpmMessage = "404 Not Found: bogusmod";
            except.NpmErrno = "{}";
            except.NpmVerbose = "'bogusmod' is not in the npm registry.\n" +
                                "You should bug the author to publish it\n" +
                                "\n" +
                                "Note that you can also install from a\n" +
                                "tarball, folder, or http url, or git url.\n" +
                                "\n" +
                                "\n" +
                                "Additional logging details can be found in:\n" +
                                "    C:\\src\\npm-debug.log\n";

            return except;
        }
    }
}
