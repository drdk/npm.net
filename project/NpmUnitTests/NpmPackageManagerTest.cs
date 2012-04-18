// -----------------------------------------------------------------------
// <copyright file="NpmPackageManagerTest.cs" company="Microsoft">
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
    /// This is a test class for NpmPackageManagerTest and is intended
    /// to contain all NpmPackageManagerTest Unit Tests
    /// </summary>
    [TestClass]
    public class NpmPackageManagerTest
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
        /// A test for NpmPackageManager Constructor
        /// </summary>
        [TestMethod]
        public void NpmPackageManagerConstructorTest()
        {
            NpmFactory factory = new MockNpmFactory();
            string wd = string.Empty;
            string registry = null;
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmPackageManager));
        }

        /// <summary>
        /// A test for NpmPackageManager Constructor
        /// </summary>
        [TestMethod]
        public void NpmPackageManagerConstructorTest1()
        {
            string wd = string.Empty;
            string registry = string.Empty;
            NpmPackageManager target = new NpmPackageManager(wd, registry);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmPackageManager));
        }

        /// <summary>
        /// A test for NpmPackageManager Constructor
        /// </summary>
        [TestMethod]
        public void NpmPackageManagerConstructorTest2()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmPackageManager));
        }

        /// <summary>
        /// A test for FindDependenciesToBeInstalled
        /// </summary>
        [TestMethod]
        public void FindDependenciesToBeInstalledTest()
        {
            string wd = "c:\\root\\project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = new NpmPackage("outdated1", null);
            List<NpmPackageDependency> expected = MockTestData.Outdated1Expected();
            IEnumerable<INpmPackageDependency> actual;
            actual = target.FindDependenciesToBeInstalled(package);
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
        /// A test for FindDependenciesToBeInstalled
        /// </summary>
        [TestMethod]
        public void FindDependenciesToBeInstalledMultiTest()
        {
            string wd = "c:\\root\\outdatedmulti";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = new NpmPackage("outdatedparent", null);
            List<NpmPackageDependency> expected = MockTestData.OutdatedMultiExpected();
            IEnumerable<INpmPackageDependency> actual;
            actual = target.FindDependenciesToBeInstalled(package);
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
        /// A test for FindPackages
        /// </summary>
        [TestMethod]
        public void FindPackagesTest()
        {
            string wd = string.Empty;
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            List<string> packageIds = new List<string>();
            packageIds.Add("view1");
            List<NpmRemotePackage> expected = new List<NpmRemotePackage>();
            expected.Add(MockTestData.View1Expected());
            IEnumerable<INpmRemotePackage> actual;
            actual = target.FindPackages(packageIds);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (INpmRemotePackage actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }

        /// <summary>
        /// A test for GetInstalledPackages
        /// </summary>
        [TestMethod]
        public void GetInstalledPackagesTest()
        {
            string wd = "c:\\root\\project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            List<NpmInstalledPackage> expected = MockTestData.List1Expected();
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.GetInstalledPackages();
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
        /// A test for GetPackagesWithUpdates
        /// </summary>
        [TestMethod]
        public void GetPackagesWithUpdatesTest()
        {
            string wd = "c:\\root\\project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            List<NpmPackageDependency> expected = MockTestData.Outdated1Expected();
            IEnumerable<INpmPackageDependency> actual;
            actual = target.GetPackagesWithUpdates();
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
        /// A test for GetRemotePackages
        /// </summary>
        [TestMethod]
        public void GetRemotePackagesTest()
        {
            string wd = string.Empty;
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            string searchTerms = null;
            List<NpmSearchResultPackage> expected = MockTestData.SearchResult2Expected();
            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.SearchRemotePackages(searchTerms);
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
        /// A test for InstallPackage
        /// </summary>
        [TestMethod]
        public void InstallPackageTest()
        {
            string wd = "c:\\root\\project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = new NpmPackage("install1", null);
            List<NpmInstalledPackage> expected = MockTestData.Install1Expected();
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.InstallPackage(package);
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
        /// A test for IsPackageInstalled
        /// </summary>
        [TestMethod]
        public void IsPackageInstalledTest()
        {
            string wd = "c:\\root\\project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = MockTestData.List1MatchInstalledPackage();
            NpmInstalledPackage expected = MockTestData.List1MatchInstalledExpected();
            INpmInstalledPackage actual;
            actual = target.IsPackageInstalled(package);
            if (expected == null)
            {
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected, actual, "item value differs");
            }
        }

        /// <summary>
        /// A test for SearchRemotePackages
        /// </summary>
        [TestMethod]
        public void SearchRemotePackagesTest()
        {
            string wd = string.Empty;
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            string searchTerms = "search1";
            List<NpmSearchResultPackage> expected = MockTestData.SearchResult1Expected();
            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.SearchRemotePackages(searchTerms);
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
        /// A test for UninstallPackage
        /// </summary>
        [TestMethod]
        public void UninstallPackageTest()
        {
            string wd = "c:\\root\\uninstall1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            INpmPackage package = new NpmPackage("uninstall1", null);
            List<string> expected = MockTestData.Uninstall1Expected();
            IEnumerable<string> actual;
            actual = target.UninstallPackage(package);
            if (expected == null)
            {
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Count, actual.Count());
                foreach (string result in actual)
                {
                    Assert.IsTrue(expected.Contains(result), "Uninstall result value differs");
                }
            }
        }

        /// <summary>
        /// A test for UpdatePackage
        /// </summary>
        [TestMethod]
        public void UpdatePackageTest()
        {
            string wd = "c:\\root\\update1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = new NpmPackage("underscore", null);
            List<string> expected = MockTestData.Install1ExpectedNames();
            IEnumerable<string> actual;
            actual = target.UpdatePackage(package);
            if (expected == null)
            {
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Count, actual.Count());
                foreach (string result in actual)
                {
                    Assert.IsTrue(expected.Contains(result), "Update result value differs");
                }
            }
        }

        /// <summary>
        /// A test for ApiClient
        /// </summary>
        [TestMethod]
        public void ApiClientTest()
        {
            string wd = string.Empty;
            NpmPackageManager target = new NpmPackageManager(wd);
            INpmApi expected = (INpmApi)new NpmApi(wd);
            INpmApi actual;
            target.ApiClient = expected;
            actual = target.ApiClient;
            Assert.AreEqual(expected, actual);
        }
    }
}
