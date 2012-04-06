using Webmatrix_Npm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace NpmUnitTests
{
    
    
    /// <summary>
    ///This is a test class for NpmPackageManagerTest and is intended
    ///to contain all NpmPackageManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NpmPackageManagerTest
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
        ///A test for NpmPackageManager Constructor
        ///</summary>
        [TestMethod()]
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
        ///A test for NpmPackageManager Constructor
        ///</summary>
        [TestMethod()]
        public void NpmPackageManagerConstructorTest1()
        {
            string wd = string.Empty;
            string registry = string.Empty;
            NpmPackageManager target = new NpmPackageManager(wd, registry);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmPackageManager));
        }

        /// <summary>
        ///A test for NpmPackageManager Constructor
        ///</summary>
        [TestMethod()]
        public void NpmPackageManagerConstructorTest2()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmPackageManager));
        }

        /// <summary>
        ///A test for FindDependenciesToBeInstalled
        ///</summary>
        [TestMethod()]
        public void FindDependenciesToBeInstalledTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            INpmInstalledPackage package = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FindDependenciesToBeInstalled(package);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FindPackages
        ///</summary>
        [TestMethod()]
        public void FindPackagesTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            IEnumerable<string> packageIds = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmRemotePackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmRemotePackage> actual;
            actual = target.FindPackages(packageIds);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetInstalledPackages
        ///</summary>
        [TestMethod()]
        public void GetInstalledPackagesTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.GetInstalledPackages();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPackagesWithUpdates
        ///</summary>
        [TestMethod()]
        public void GetPackagesWithUpdatesTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackageDependency> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackageDependency> actual;
            actual = target.GetPackagesWithUpdates();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRemotePackages
        ///</summary>
        [TestMethod()]
        public void GetRemotePackagesTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            IEnumerable<INpmSearchResultPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.GetRemotePackages();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InstallPackage
        ///</summary>
        [TestMethod()]
        public void InstallPackageTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            INpmPackage package = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackage> actual;
            actual = target.InstallPackage(package);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsPackageInstalled
        ///</summary>
        [TestMethod()]
        public void IsPackageInstalledTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            INpmPackage package = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsPackageInstalled(package);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SearchRemotePackages
        ///</summary>
        [TestMethod()]
        public void SearchRemotePackagesTest()
        {
            string wd = string.Empty;
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            string searchTerms = "azure";
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
                new string[] { });
            expected.Add(res);
            expected.Add(res2);
            expected.Add(res3);
            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.SearchRemotePackages(searchTerms);
            Assert.IsNotNull(actual);
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
        ///A test for UninstallPackage
        ///</summary>
        [TestMethod()]
        public void UninstallPackageTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            INpmPackage package = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = target.UninstallPackage(package);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdatePackage
        ///</summary>
        [TestMethod()]
        public void UpdatePackageTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            INpmPackage package = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = target.UpdatePackage(package);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ApiClient
        ///</summary>
        [TestMethod()]
        public void ApiClientTest()
        {
            string wd = string.Empty; // TODO: Initialize to an appropriate value
            NpmPackageManager target = new NpmPackageManager(wd); // TODO: Initialize to an appropriate value
            INpmApi expected = null; // TODO: Initialize to an appropriate value
            INpmApi actual;
            target.ApiClient = expected;
            actual = target.ApiClient;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
