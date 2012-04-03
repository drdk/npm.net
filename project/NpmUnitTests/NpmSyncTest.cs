using Webmatrix_Npm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace NpmUnitTests
{
    
    
    /// <summary>
    ///This is a test class for NpmSyncTest and is intended
    ///to contain all NpmSyncTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NpmSyncTest
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
        ///A test for NpmSync Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("webmatrix_npm.dll")]
        public void NpmSyncConstructorTest()
        {
            Process npm = null; // TODO: Initialize to an appropriate value
            NpmSync_Accessor target = new NpmSync_Accessor(npm);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddNpmSync
        ///</summary>
        [TestMethod()]
        public void AddNpmSyncTest()
        {
            Process obj = null; // TODO: Initialize to an appropriate value
            NpmSync.AddNpmSync(obj);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddToError
        ///</summary>
        [TestMethod()]
        public void AddToErrorTest()
        {
            using (Process proc = new Process())
            {
                NpmSync_Accessor target = new NpmSync_Accessor(proc); // TODO: Initialize to an appropriate value
                string line = string.Empty; // TODO: Initialize to an appropriate value
                target.AddToError(line);
                Assert.Inconclusive("A method that does not return a value cannot be verified.");
            }
        }

        /// <summary>
        ///A test for AddToOuput
        ///</summary>
        [TestMethod()]
        public void AddToOuputTest()
        {
            using (Process proc = new Process())
            {
                NpmSync_Accessor target = new NpmSync_Accessor(proc); // TODO: Initialize to an appropriate value
                string line = string.Empty; // TODO: Initialize to an appropriate value
                target.AddToOuput(line);
                Assert.Inconclusive("A method that does not return a value cannot be verified.");
            }
        }

        /// <summary>
        ///A test for FindNpmSync
        ///</summary>
        [TestMethod()]
        public void FindNpmSyncTest()
        {
            Process obj = null; // TODO: Initialize to an appropriate value
            NpmSync expected = null; // TODO: Initialize to an appropriate value
            NpmSync actual;
            actual = NpmSync.FindNpmSync(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetError
        ///</summary>
        [TestMethod()]
        public void GetErrorTest()
        {
            using (Process proc = new Process())
            {
                NpmSync_Accessor target = new NpmSync_Accessor(proc); // TODO: Initialize to an appropriate value
                string expected = string.Empty; // TODO: Initialize to an appropriate value
                string actual;
                actual = target.GetError();
                Assert.AreEqual(expected, actual);
                Assert.Inconclusive("Verify the correctness of this test method.");
            }
        }

        /// <summary>
        ///A test for GetOutput
        ///</summary>
        [TestMethod()]
        public void GetOutputTest()
        {
            using (Process proc = new Process())
            {
                NpmSync_Accessor target = new NpmSync_Accessor(proc); // TODO: Initialize to an appropriate value
                string expected = string.Empty; // TODO: Initialize to an appropriate value
                string actual;
                actual = target.GetOutput();
                Assert.AreEqual(expected, actual);
                Assert.Inconclusive("Verify the correctness of this test method.");
            }
        }

        /// <summary>
        ///A test for RemNpmSync
        ///</summary>
        [TestMethod()]
        public void RemNpmSyncTest()
        {
            Process obj = null; // TODO: Initialize to an appropriate value
            NpmSync.RemNpmSync(obj);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
