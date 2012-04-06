// -----------------------------------------------------------------------
// <copyright file="MockTestData.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NpmUnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Webmatrix_Npm;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class MockTestData
    {
        /// <summary>
        /// Npm text output for install
        /// </summary>
        /// <returns></returns>
        public static string Install1Text()
        {
            return "underscore@1.3.1 ./node_modules/underscore\n";
        }

        /// <summary>
        /// Expected output from install
        /// </summary>
        /// <returns></returns>
        public static List<NpmPackage> Install1Expected()
        {
            List<NpmPackage> expected = new List<NpmPackage>();
            NpmPackage package = new NpmPackage("underscore", "1.3.1");
            expected.Add(package);
            return expected;
        }

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

        public static NpmInstalledPackage List1Expected()
        {
            NpmInstalledPackage expected = new NpmInstalledPackage();
            expected.Name = "azure";
            expected.Version = "0.5.2";
            List<INpmInstalledPackage> depends = new List<INpmInstalledPackage>();
            expected.MissingDependencies = null;
            expected.OutdatedDependencies = null;
            NpmInstalledPackage depend = new NpmInstalledPackage();
            depend.Name = "xml2js";
            depend.Version = "0.1.13";
            depends.Add(depend);
            depend = new NpmInstalledPackage();
            depend.Name = "sax";
            depend.Version = "0.4.0";
            depends.Add(depend);
            depend = new NpmInstalledPackage();
            depend.Name = "jshint";
            depend.Version = "0.5.9";
            depends.Add(depend);
            List<INpmInstalledPackage> jshintDepends = new List<INpmInstalledPackage>();
            NpmInstalledPackage jshintDepend = new NpmInstalledPackage();
            jshintDepend.Name = "argsparser";
            jshintDepend.Version = "0.0.6";
            jshintDepends.Add(jshintDepend);
            jshintDepend.InstalledDependencies = jshintDepends;
            expected.InstalledDependencies = depends;
            return expected;
        }

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

        public static NpmInstalledPackage ListProblem1Expected()
        {
            NpmInstalledPackage expected = new NpmInstalledPackage();
            expected.Name = "azure";
            expected.Version = "0.5.2";
            List<INpmInstalledPackage> depends = new List<INpmInstalledPackage>();
            expected.MissingDependencies = null;
            expected.OutdatedDependencies = null;
            NpmInstalledPackage depend = new NpmInstalledPackage();
            depend.Name = "xml2js";
            depend.Version = "0.1.13";
            depends.Add(depend);
            depend = new NpmInstalledPackage();
            expected.InstalledDependencies = depends;

            List<NpmPackageDependency> badDepends = new List<NpmPackageDependency>();
            NpmPackageDependency badDepend = new NpmPackageDependency();
            badDepend.Name = "mime";
            badDepend.VersionRange = ">= 1.2.4";
            badDepends.Add(badDepend);
            expected.MissingDependencies = badDepends;

            badDepends = new List<NpmPackageDependency>();
            badDepend = new NpmPackageDependency();
            badDepend.Name = "underscore";
            badDepend.Version = "1.3.0";
            badDepends.Add(badDepend);
            expected.OutdatedDependencies = badDepends;
            return expected;
        }

        public static string Outdated1Text()
        {
            return "underscore@1.3.1 ./node_modules/underscore current=1.3.0\n";
        }

        public static NpmPackageDependency OutdatedSingle1Expected()
        {
            NpmPackageDependency dependency = new NpmPackageDependency();
            dependency.Name = "underscore";
            dependency.Version = "1.3.0";
            dependency.VersionRange = "1.3.1";
            return dependency;
        }

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

        public static string SearchResult1Text()
        {
            return
                "NAME                  DESCRIPTION                                                   AUTHOR            DATE              KEYWORDS\n" +
                "azure                 Windows Azure Client Library for node                         =andrerod         2012-02-16 05:16  node azure\n" +
                "node-swt              A library to validate and parse swt tokens                    =dario.renzulli   2012-01-18 01:07  swt acs security azure\n" +
                "node_in_windows_azure An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012 =tomgallacher 2012-01-25 15:19\n";
        }

        public static List<NpmSearchResultPackage> SearchResult1Expected()
        {
            List<NpmSearchResultPackage> expected = new List<NpmSearchResultPackage>();
            NpmSearchResultPackage res = new NpmSearchResultPackage("azure",
                null,
                "Windows Azure Client Library for node",
                "andrerod",
                new DateTime(2012, 2, 16, 5, 16, 0),
                new string[] { "node", "azure" });
            NpmSearchResultPackage res2 = new NpmSearchResultPackage("node-swt",
                null,
                "A library to validate and parse swt tokens",
                "dario.renzulli",
                new DateTime(2012, 1, 18, 1, 7, 0),
                new string[] { "swt", "acs", "security", "azure" });
            NpmSearchResultPackage res3 = new NpmSearchResultPackage("node_in_windows_azure",
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
            expected.HomepageUrl = "https://github.com/felixge/node-dateformat";
            expected.Author = "Steven Levithan";
            expected.Dependencies = new List<NpmPackageDependency>();
            expected.DevDependencies = new List<NpmPackageDependency>();
            expected.OptionalDependencies = null;
            expected.License = null;
            expected.Repository = null;
            return expected;
        }

        public static string Version1Text()
        {
            return "1.1.9\n\n";
        }

        public static string Version1Expected()
        {
            return "1.1.9";
        }

    }
}
