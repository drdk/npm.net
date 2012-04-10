namespace NpmUnitTests
{
    using NodejsNpm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            string searchTerms = "search1";
            List<NpmSearchResultPackage> expected = MockTestData.SearchResult1Expected();
            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.SearchRemotePackages(searchTerms);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count<INpmSearchResultPackage>());
            int index = 0;
            foreach (INpmSearchResultPackage result in actual)
            {
                string diff;
                Assert.IsTrue(expected[index].IsSame(result, out diff), "Search result value differs in " + diff);
                index++;
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
