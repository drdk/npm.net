namespace NpmUnitTests
{
    using Webmatrix_Npm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///This is a test class for NpmApiTest and is intended
    ///to contain all NpmApiTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NpmApiTest
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
        ///A test for NpmApi Constructor
        ///</summary>
        [TestMethod()]
        public void NpmApiConstructorTest()
        {
            string wd = string.Empty;
            string registry = string.Empty;
            NpmApi target = new NpmApi(wd, registry);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmApi));
        }

        /// <summary>
        ///A test for GetInstalledVersion
        ///</summary>
        [TestMethod()]
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
        ///A test for Install
        ///</summary>
        [TestMethod()]
        public void InstallTest()
        {
            string wd = string.Empty;
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            NpmPackage package = new NpmPackage("install1", null);
            List<NpmPackage> expected = MockTestData.Install1Expected();
            IEnumerable<INpmPackage> actual;
            actual = target.Install(package);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count<INpmPackage>());
            int index = 0;
            foreach (INpmPackage actualItem in actual)
            {
                string diff;
                Assert.IsTrue(expected[index].IsSame(actualItem, out diff), "Install value differs in " + diff);
                index++;
            }
        }

        /// <summary>
        ///A test for List
        ///</summary>
        [TestMethod()]
        public void ListTest()
        {
            string wd = "/root/project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            NpmInstalledPackage expected = MockTestData.List1Expected();
            INpmInstalledPackage actual;
            actual = target.List();
            Assert.IsNotNull(actual);
            string diff;
            Assert.IsTrue(expected.IsSame(actual, out diff), "ListInstalled value differs in " + diff);
        }

        /// <summary>
        ///A test for Outdated
        ///</summary>
        [TestMethod()]
        public void OutdatedTest()
        {
            string wd = "/root/project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string name = "outdated1";
            NpmPackageDependency expected = MockTestData.OutdatedSingle1Expected();
            INpmPackageDependency actual;
            actual = target.Outdated(name);
            string diff;
            Assert.IsTrue(expected.IsSame(actual, out diff), "Outdated package dependency value differs in " + diff);
        }

        /// <summary>
        ///A test for Outdated
        ///</summary>
        [TestMethod()]
        public void OutdatedTest1()
        {
            string wd = "/root/project1";
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            List<NpmPackageDependency> expected = MockTestData.Outdated1Expected();
            IEnumerable<INpmPackageDependency> actual;
            actual = target.Outdated();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count<INpmPackageDependency>());
            int index = 0;
            foreach (INpmPackageDependency actualItem in actual)
            {
                string diff;
                Assert.IsTrue(expected[index].IsSame(actualItem, out diff), "Outdated package dependency value differs in " + diff);
                index++;
            }
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
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
        ///A test for Uninstall
        ///</summary>
        [TestMethod()]
        public void UninstallTest()
        {
            string wd = "/root/project1";
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
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            string wd = "/root/project1";
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, wd, registry);
            string name = "update1";
            bool expected = true;
            bool actual;
            actual = target.Update(name);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for View
        ///</summary>
        [TestMethod()]
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
            string diff;
            Assert.IsTrue(expected.IsSame(actual, out diff), "View value differs in " + diff);
        }
    }
}
