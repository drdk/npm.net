using Webmatrix_Npm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace NpmUnitTests
{
    
    
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
            string cwd = string.Empty;
            string registry = string.Empty;
            NpmApi target = new NpmApi(cwd, registry);
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(NpmApi));
        }

        /// <summary>
        ///A test for GetInstalledVersion
        ///</summary>
        [TestMethod()]
        public void GetInstalledVersionTest()
        {
            string cwd = string.Empty;
            string registry = string.Empty;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, cwd, registry);
            string expected = "1.1.9";
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
            string cwd = string.Empty; // TODO: Initialize to an appropriate value
            string registry = string.Empty; // TODO: Initialize to an appropriate value
            NpmApi target = new NpmApi(cwd, registry); // TODO: Initialize to an appropriate value
            INpmPackage package = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Install(package);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for List
        ///</summary>
        [TestMethod()]
        public void ListTest()
        {
            string cwd = string.Empty; // TODO: Initialize to an appropriate value
            string registry = string.Empty; // TODO: Initialize to an appropriate value
            NpmApi target = new NpmApi(cwd, registry); // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmInstalledPackage> actual;
            actual = target.List();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Outdated
        ///</summary>
        [TestMethod()]
        public void OutdatedTest()
        {
            string cwd = string.Empty; // TODO: Initialize to an appropriate value
            string registry = string.Empty; // TODO: Initialize to an appropriate value
            NpmApi target = new NpmApi(cwd, registry); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackage> expected = null; // TODO: Initialize to an appropriate value
            INpmPackage actual;
            actual = target.Outdated(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Outdated
        ///</summary>
        [TestMethod()]
        public void OutdatedTest1()
        {
            string cwd = string.Empty; // TODO: Initialize to an appropriate value
            string registry = string.Empty; // TODO: Initialize to an appropriate value
            NpmApi target = new NpmApi(cwd, registry); // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackage> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<INpmPackage> actual;
            actual = target.Outdated();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest()
        {
            string cwd = string.Empty;
            string registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmApi target = new NpmApi(factory, cwd, registry);
            string searchTerms = "azure";
            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.Search(searchTerms);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Uninstall
        ///</summary>
        [TestMethod()]
        public void UninstallTest()
        {
            string cwd = string.Empty; // TODO: Initialize to an appropriate value
            string registry = string.Empty; // TODO: Initialize to an appropriate value
            NpmApi target = new NpmApi(cwd, registry); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Uninstall(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            string cwd = string.Empty; // TODO: Initialize to an appropriate value
            string registry = string.Empty; // TODO: Initialize to an appropriate value
            NpmApi target = new NpmApi(cwd, registry); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Update(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for View
        ///</summary>
        [TestMethod()]
        public void ViewTest()
        {
            string cwd = string.Empty; // TODO: Initialize to an appropriate value
            string registry = string.Empty; // TODO: Initialize to an appropriate value
            NpmApi target = new NpmApi(cwd, registry); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            INpmRemotePackage expected = null; // TODO: Initialize to an appropriate value
            INpmRemotePackage actual;
            actual = target.View(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
