// -----------------------------------------------------------------------
// <copyright file="NpmPackageManagerTest.cs" company="Microsoft Open Technologies, Inc.">
// Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0.
//
// THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT
// LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR
// A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache License, Version 2.0 for specific language governing
// permissions and limitations under the License.
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
            Uri registry = null;
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
            Uri registry = null;
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
            Uri registry = null;
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
        public void FindDependenciesToBeInstalledEmptyTest()
        {
            string wd = "c:\\root\\outdatedmulti";
            Uri registry = null;
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
            Uri registry = null;
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
            Uri registry = null;
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
            Uri registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            List<NpmInstalledPackage> expected = MockTestData.List1ChildrenExpected();
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
        /// A test for GetInstalledPackages
        /// </summary>
        [TestMethod]
        public void GetInstalledPackagesEmptyTest()
        {
            string wd = "c:\\root\\empty1";
            Uri registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            List<NpmInstalledPackage> expected = MockTestData.ListEmptyExpected();
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
            string wd = "c:\\root\\update1";
            Uri registry = null;
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
            Uri registry = null;
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
            Uri registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = new NpmPackage("install1", null);
            target.InstallPackage(package);
            Assert.IsTrue(true);   // no exception thrown
        }

        /// <summary>
        /// A test for IsPackageInstalled
        /// </summary>
        [TestMethod]
        public void IsPackageInstalledTest()
        {
            string wd = "c:\\root\\project1";
            Uri registry = null;
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
        /// A test for IsPackageInstalled on grandchild - expect failure
        /// </summary>
        [TestMethod]
        public void IsPackageInstalledFailTest()
        {
            string wd = "c:\\root\\project2";
            Uri registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = MockTestData.List1MatchInstalledPackage();
            NpmInstalledPackage expected = null;
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
            Uri registry = null;
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
            Uri registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            INpmPackage package = new NpmPackage("uninstall1", null);
            target.UninstallPackage(package);
            Assert.IsTrue(true);    // no exception thrown
        }

        /// <summary>
        /// A test for UpdatePackage
        /// </summary>
        [TestMethod]
        public void UpdatePackageTest()
        {
            string wd = "c:\\root\\update1";
            Uri registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = new NpmPackage("underscore", null);
            target.UpdatePackage(package);
            Assert.IsTrue(true);    // no exception thrown
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

        /// <summary>
        /// A test for InstallPackage exception
        /// </summary>
        [TestMethod]
        public void InstallPackageFailureTest()
        {
            string wd = "c:\\root\\project1";
            Uri registry = null;
            NpmFactory factory = new MockNpmFactory();
            NpmPackageManager target = new NpmPackageManager(factory, wd, registry);
            NpmPackage package = new NpmPackage("bogusmod", null);
            NpmException expected = MockTestData.ErrorInstallExpected();
            try
            {
                target.InstallPackage(package);
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
