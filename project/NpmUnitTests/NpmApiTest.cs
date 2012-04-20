// -----------------------------------------------------------------------
// <copyright file="NpmApiTest.cs" company="Microsoft">
// Class for npm package manager unit tests
// </copyright>
// -----------------------------------------------------------------------

namespace NpmUnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NodeNpm;

    /// <summary>
    /// This is a test class for NpmApiTest and is intended
    /// to contain all NpmApiTest Unit Tests
    /// </summary>
    [TestClass]
    public class NpmApiTest
    {
        /// <summary>
        /// Value used by test framework
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
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
        /// A test for NpmApi Constructor
        /// </summary>
        [TestMethod]
        public void NpmApiConstructorTest()
        {
            string wd = string.Empty;
            string registry = string.Empty;
            NpmApi target = new NpmApi(wd, registry);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmApi));
        }

        /// <summary>
        /// A test for GetInstalledVersion
        /// </summary>
        [TestMethod]
        public void GetInstalledVersionTest()
        {
            string wd = string.Empty;
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string expected = MockTestData.Version1Expected();
            string actual;
            actual = target.GetInstalledVersion();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Install
        /// </summary>
        [TestMethod]
        public void InstallTest()
        {
            string wd = "c:\\root\\project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            NpmPackage package = new NpmPackage("install1", null);
            List<NpmInstalledPackage> expected = MockTestData.Install1Expected();
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.Install(package);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (INpmInstalledPackage actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for List
        /// </summary>
        [TestMethod]
        public void ListTest()
        {
            string wd = "c:\\root\\project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            List<NpmInstalledPackage> expected = MockTestData.List1Expected();
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.List();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (NpmInstalledPackage actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for List
        /// </summary>
        [TestMethod]
        public void ListEmptyTest()
        {
            string wd = "c:\\root\\empty1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            List<NpmInstalledPackage> expected = MockTestData.ListEmptyExpected();
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.List();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (NpmInstalledPackage actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for Outdated
        /// </summary>
        [TestMethod]
        public void OutdatedTest()
        {
            string wd = "c:\\root\\project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string name = "outdated1";
            List<NpmPackageDependency> expected = MockTestData.Outdated1Expected();
            IEnumerable<INpmPackageDependency> actual;
            actual = target.Outdated(name);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (INpmPackageDependency actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for Outdated
        /// </summary>
        [TestMethod]
        public void OutdatedTest1()
        {
            string wd = "c:\\root\\project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            List<NpmPackageDependency> expected = MockTestData.Outdated1Expected();
            IEnumerable<INpmPackageDependency> actual;
            actual = target.Outdated();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (INpmPackageDependency actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for Search
        /// </summary>
        [TestMethod]
        public void SearchTest()
        {
            string wd = string.Empty;
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string searchTerms = "search1";
            List<NpmSearchResultPackage> expected = MockTestData.SearchResult1Expected();
            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.Search(searchTerms);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (INpmSearchResultPackage actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for Uninstall
        /// </summary>
        [TestMethod]
        public void UninstallTest()
        {
            string wd = "c:\\root\\project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string name = "uninstall1";
            bool expected = true;
            bool actual;
            actual = target.Uninstall(name);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Update
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            string wd = "c:\\root\\project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string name = "underscore";
            List<NpmInstalledPackage> expected = MockTestData.Install1Expected();
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.Update(name);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (INpmInstalledPackage actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for View
        /// </summary>
        [TestMethod]
        public void ViewTest()
        {
            string wd = string.Empty;
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string name = "view1";
            NpmRemotePackage expected = MockTestData.View1Expected();
            INpmRemotePackage actual;
            actual = target.View(name);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for TestInstalled
        /// </summary>
        [TestMethod]
        public void TestInstalledTest()
        {
            string wd = "c:\\root\\project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            NpmPackage package = MockTestData.List1MatchInstalledPackage();
            NpmInstalledPackage expected = MockTestData.List1MatchInstalledExpected();
            INpmInstalledPackage actual;
            actual = target.TestInstalled(package);
            if (expected == null)
            {
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        /// A test for Install failure
        /// </summary>
        [TestMethod]
        public void InstallFailsTest()
        {
            string wd = "c:\\root\\project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            NpmPackage package = new NpmPackage("bogusmod", null);
            NpmException expected = MockTestData.ErrorInstallExpected();
            try
            {
                IEnumerable<INpmInstalledPackage> actual;
                actual = target.Install(package);
                Assert.Fail("Expected exception");
            }
            catch (NpmException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual(expected.Message, ex.Message);
                Assert.AreEqual(expected.NpmCode, ex.NpmCode);
                Assert.AreEqual(expected.NpmErrno, ex.NpmErrno);
                Assert.AreEqual(expected.NpmFile, ex.NpmFile);
                Assert.AreEqual(expected.NpmPath, ex.NpmPath);
                Assert.AreEqual(expected.NpmType, ex.NpmType);
                Assert.AreEqual(expected.NpmSyscall, ex.NpmSyscall);
                Assert.AreEqual(expected.NpmSystem, ex.NpmSystem);
                Assert.AreEqual(expected.NpmCommand, ex.NpmCommand);
                Assert.AreEqual(expected.NpmNodeVersion, ex.NpmNodeVersion);
                Assert.AreEqual(expected.NpmNpmVersion, ex.NpmNpmVersion);
                Assert.AreEqual(expected.NpmMessage, ex.NpmMessage);
                Assert.AreEqual(expected.NpmArguments, ex.NpmArguments);
                Assert.AreEqual(expected.NpmCwd, ex.NpmCwd);
                Assert.AreEqual(expected.NpmVerbose, ex.NpmVerbose);
            }
        }
    }
}
