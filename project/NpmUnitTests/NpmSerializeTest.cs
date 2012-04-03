using Webmatrix_Npm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace NpmUnitTests
{
    
    
    /// <summary>
    ///This is a test class for NpmSerializeTest and is intended
    ///to contain all NpmSerializeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NpmSerializeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for NpmSerialize Constructor
        ///</summary>
        [TestMethod()]
        public void NpmSerializeConstructorTest()
        {
            NpmSerialize target = new NpmSerialize();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for FromInstall
        ///</summary>
        [TestMethod()]
        public void FromInstallTest()
        {
            NpmSerialize target = new NpmSerialize(); // TODO: Initialize to an appropriate value
            string output = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackage> actual;
            actual = target.FromInstall(output);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FromListInstalled
        ///</summary>
        [TestMethod()]
        public void FromListInstalledTest()
        {
            NpmSerialize target = new NpmSerialize(); // TODO: Initialize to an appropriate value
            string jsonlist = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListInstalled(jsonlist);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FromListMissing
        ///</summary>
        [TestMethod()]
        public void FromListMissingTest()
        {
            NpmSerialize target = new NpmSerialize(); // TODO: Initialize to an appropriate value
            string jsonlist = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListMissing(jsonlist);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FromListOutdated
        ///</summary>
        [TestMethod()]
        public void FromListOutdatedTest()
        {
            NpmSerialize target = new NpmSerialize(); // TODO: Initialize to an appropriate value
            string jsonlist = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListOutdated(jsonlist);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FromOutdatedDependency
        ///</summary>
        [TestMethod()]
        public void FromOutdatedDependencyTest()
        {
            NpmSerialize target = new NpmSerialize(); // TODO: Initialize to an appropriate value
            string outdated = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackageDependency> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackageDependency> actual;
            actual = target.FromOutdatedDependency(outdated);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FromSearchResult
        ///</summary>
        [TestMethod()]
        public void FromSearchResultTest()
        {
            NpmSerialize target = new NpmSerialize(); // TODO: Initialize to an appropriate value
            string output =
"NAME                  DESCRIPTION                                                   AUTHOR            DATE              KEYWORDS\n" +
"azure                 Windows Azure Client Library for node                         =andrerod         2012-02-16 05:16  node azure\n" +
"node-swt              A library to validate and parse swt tokens                    =dario.renzulli   2012-01-18 01:07  swt acs security azure\n" +
"node_in_windows_azure An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012 =tomgallacher 2012-01-25 15:19\n";

            List<INpmSearchResultPackage> expected = new List<INpmSearchResultPackage>();
            INpmSearchResultPackage res = new NpmSearchResultPackage("azure",
                null,
                "Windows Azure Client Library for node",
                "andrerod",
                new DateTime(2012, 2, 16, 5, 16, 0),
                new string[] { "node", "azure" });
            INpmSearchResultPackage res2 = new NpmSearchResultPackage("node-swt",
                null,
                "A library to validate and parse swt tokens",
                "dario.renzulli",
                new DateTime(2012, 1, 18, 1, 7, 0),
                new string[] { "swt", "acs", "security", "azure" });
            INpmSearchResultPackage res3 = new NpmSearchResultPackage("node_in_windows_azure",
                null,
                "An NPM module for the Windows Azure t-shirts handed out at #NodeSummit 2012",
                "tomgallacher",
                new DateTime(2012, 1, 25, 15, 19, 0),
                new string[] {});
            expected.Add(res);
            expected.Add(res2);
            expected.Add(res3);

            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.FromSearchResult(output);
            int ix = 0;
            foreach (INpmSearchResultPackage result in actual)
            {
                Assert.AreEqual(expected[ix].Author, result.Author);
                Assert.AreEqual(expected[ix].Description, result.Description);
                Assert.AreEqual(expected[ix].Date, result.Date);
                Assert.AreEqual(expected[ix].Name, result.Name);
                Assert.AreEqual(expected[ix].Keywords.Length, result.Keywords.Length);
                ix++;
            }
        }

        /// <summary>
        ///A test for FromView
        ///</summary>
        [TestMethod()]
        public void FromViewTest()
        {
            NpmSerialize target = new NpmSerialize(); // TODO: Initialize to an appropriate value
            string jsonview = string.Empty; // TODO: Initialize to an appropriate value
            INpmRemotePackage expected = null; // TODO: Initialize to an appropriate value
            INpmRemotePackage actual;
            actual = target.FromView(jsonview);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
