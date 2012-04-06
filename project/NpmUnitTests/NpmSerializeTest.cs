namespace NpmUnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Webmatrix_Npm;

    
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
            IEnumerable<INpmPackage> actual;
            NpmSerialize target = new NpmSerialize();

            string output = MockTestData.Install1Text();
            List<NpmPackage> expected = MockTestData.Install1Expected();

            actual = target.FromInstall(output);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count<INpmPackage>());
            int index = 0;
            foreach (INpmPackage actualItem in actual)
            {
                string diff;
                Assert.IsTrue(expected[index].IsSame(actualItem, out diff), "ListInstalled value differs in " + diff);
                index++;
            }
        }

        /// <summary>
        ///A test for FromListInstalled
        ///</summary>
        [TestMethod()]
        public void FromListInstalledTest()
        {
            NpmSerialize target = new NpmSerialize();

            string jsonlist = MockTestData.List1Text();
            NpmInstalledPackage expected = MockTestData.List1Expected();

            INpmInstalledPackage actual;
            actual = target.FromListInstalled(jsonlist);
            Assert.IsNotNull(actual);
            string diff;
            Assert.IsTrue(expected.IsSame(actual, out diff), "ListInstalled value differs in " + diff);
        }

        /// <summary>
        ///A test for FromListInstalled
        ///</summary>
        [TestMethod()]
        public void FromListInstalledTest2()
        {
            NpmSerialize target = new NpmSerialize();

            string jsonlist = MockTestData.ListProblems1Text();
            NpmInstalledPackage expected = MockTestData.ListProblem1Expected();

            INpmInstalledPackage actual;
            actual = target.FromListInstalled(jsonlist);
            Assert.IsNotNull(actual);
            string diff;
            Assert.IsTrue(expected.IsSame(actual, out diff), "ListInstalled value differs in " + diff);
        }

        /// <summary>
        ///A test for FromOutdatedDependency
        ///</summary>
        [TestMethod()]
        public void FromOutdatedDependencyTest()
        {
            NpmSerialize target = new NpmSerialize();

            string outdated = MockTestData.Outdated1Text();
            List<NpmPackageDependency> expected = MockTestData.Outdated1Expected();

            IEnumerable<INpmPackageDependency> actual;
            actual = target.FromOutdatedDependency(outdated);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count<INpmPackageDependency>());
            int index = 0;
            foreach (INpmPackageDependency actualItem in actual)
            {
                string diff;
                Assert.IsTrue(expected[index].IsSame(actualItem, out diff), "ListInstalled value differs in " + diff);
                index++;
            }
        }

        /// <summary>
        ///A test for FromSearchResult
        ///</summary>
        [TestMethod()]
        public void FromSearchResultTest()
        {
            NpmSerialize target = new NpmSerialize();
            string output = MockTestData.SearchResult1Text();
            List<NpmSearchResultPackage> expected = MockTestData.SearchResult1Expected();

            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.FromSearchResult(output);
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
        ///A test for FromView
        ///</summary>
        [TestMethod()]
        public void FromViewTest()
        {
            NpmSerialize target = new NpmSerialize();

            string jsonview = MockTestData.View1Text();
            NpmRemotePackage expected = MockTestData.View1Expected();
            INpmRemotePackage actual;
            actual = target.FromView(jsonview);
            string diff;
            Assert.IsTrue(expected.IsSame(actual, out diff), "View value differs in " + diff);
        }
    }
}
