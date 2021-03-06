﻿// -----------------------------------------------------------------------
// <copyright file="NpmSerializeTest.cs" company="Microsoft Open Technologies, Inc.">
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
    /// This is a test class for NpmSerializeTest and is intended
    /// to contain all NpmSerializeTest Unit Tests
    /// </summary>
    [TestClass]
    public class NpmSerializeTest
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
        /// A test for NpmSerialize Constructor
        /// </summary>
        [TestMethod]
        public void NpmSerializeConstructorTest()
        {
            NpmSerialize target = new NpmSerialize();
            Assert.IsNotNull(target);
            NpmSerialize_v2 target2 = new NpmSerialize_v2();
            Assert.IsNotNull(target2);
        }

        /// <summary>
        /// A test for NpmSerialize Constructor
        /// </summary>
        [TestMethod]
        public void NpmSerializeFactoryConstructorTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// A test for FromInstall
        /// </summary>
        [TestMethod]
        public void FromInstallTest()
        {
            IEnumerable<INpmInstalledPackage> actual;
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string output = MockTestData.Install1Text();
            List<NpmInstalledPackage> expected = MockTestData.Install1Expected();

            actual = target.FromInstall(output);
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
        /// A test for FromInstall
        /// </summary>
        [TestMethod]
        public void FromInstallWithPrependedTextTest()
        {
            IEnumerable<INpmInstalledPackage> actual;
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string output = MockTestData.InstallWithPrependedText();
            List<NpmInstalledPackage> expected = MockTestData.Install1Expected();

            actual = target.FromInstall(output);
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
        /// A test for FromListInstalled
        /// </summary>
        [TestMethod]
        public void FromListInstalledTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.List1Text();
            List<NpmInstalledPackage> expected = MockTestData.List1Expected();

            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListInstalled(jsonlist);
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
        /// A test for FromListInstalled
        /// </summary>
        [TestMethod]
        public void FromListInstalled2Test()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.List2Text();
            List<NpmInstalledPackage> expected = MockTestData.List2Expected();

            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListInstalled(jsonlist);
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
        /// A test for FromListInstalled
        /// </summary>
        [TestMethod]
        public void FromListInstalledProblemTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.ListProblems1Text();
            List<NpmInstalledPackage> expected = MockTestData.ListProblem1Expected();

            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListInstalled(jsonlist);
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
        /// A test for FromListInstalled
        /// </summary>
        [TestMethod]
        public void FromListMatchInstalledTest1()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.List1Text();
            NpmPackage package = MockTestData.List1MatchInstalledPackage();
            NpmInstalledPackage expected = MockTestData.List1MatchInstalledExpected();

            INpmInstalledPackage actual;
            actual = target.FromListMatchInstalled(jsonlist, package);
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
        /// A test for FromListInstalled
        /// </summary>
        [TestMethod]
        public void FromListMatchInstalledTest2()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.List1Text();
            NpmPackage package = MockTestData.List2MatchInstalledPackage();
            NpmInstalledPackage expected = MockTestData.List2MatchInstalledExpected();

            INpmInstalledPackage actual;
            actual = target.FromListMatchInstalled(jsonlist, package);
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
        /// A test for FromListInstalledChildren
        /// </summary>
        [TestMethod]
        public void FromListInstalledChildrenTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.List1Text();
            List<NpmInstalledPackage> expected = MockTestData.List1ChildrenExpected();

            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListInstalledChildren(jsonlist);
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
        /// A test for FromListInstalledChildren
        /// </summary>
        [TestMethod]
        public void FromListInstalledChildren2Test()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.List2Text();
            List<NpmInstalledPackage> expected = MockTestData.List2ChildrenExpected();

            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListInstalledChildren(jsonlist);
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
        /// A test for FromOutdatedDependency
        /// </summary>
        [TestMethod]
        public void FromOutdatedDependencyTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string outdated = MockTestData.Outdated1Text();
            List<NpmPackageDependency> expected = MockTestData.Outdated1Expected();

            IEnumerable<INpmPackageDependency> actual;
            actual = target.FromOutdatedDependency(outdated);
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
        /// A test for FromSearchResult
        /// </summary>
        [TestMethod]
        public void FromSearchResultTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);
            string output = MockTestData.SearchResult1Text();
            List<NpmSearchResultPackage> expected = MockTestData.SearchResult1Expected();

            IEnumerable<INpmSearchResultPackage> actual;
            actual = target.FromSearchResult(output);
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
        /// A test for FromView
        /// </summary>
        [TestMethod]
        public void FromViewTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonview = MockTestData.View1Text();
            NpmRemotePackage expected = MockTestData.View1Expected();
            INpmRemotePackage actual;
            actual = target.FromView(jsonview);
            Assert.AreEqual(expected, actual, "item value differs");
        }

        /// <summary>
        /// A test for FromList
        /// </summary>
        [TestMethod]
        public void FromListEmptyTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string jsonlist = MockTestData.ListEmptyText();
            List<NpmInstalledPackage> expected = MockTestData.ListEmptyExpected();

            IEnumerable<INpmInstalledPackage> actual;
            actual = target.FromListInstalled(jsonlist);
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
        /// A test for ExceptionFromError
        /// </summary>
        [TestMethod]
        public void ExceptionFromErrorTest()
        {
            INpmSerialize target = new MockNpmFactory().GetSerialize(null);

            string errorText = MockTestData.ErrorInstallText();
            NpmException expected = MockTestData.ErrorInstallExpected();

            NpmException actual;
            actual = target.ExceptionFromError(errorText);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.NpmCode, actual.NpmCode);
            Assert.AreEqual(expected.NpmErrno, actual.NpmErrno);
            Assert.AreEqual(expected.NpmFile, actual.NpmFile);
            Assert.AreEqual(expected.NpmPath, actual.NpmPath);
            Assert.AreEqual(expected.NpmType, actual.NpmType);
            Assert.AreEqual(expected.NpmSyscall, actual.NpmSyscall);
            Assert.AreEqual(expected.NpmSystem, actual.NpmSystem);
            Assert.AreEqual(expected.NpmCommand, actual.NpmCommand);
            Assert.AreEqual(expected.NpmNodeVersion, actual.NpmNodeVersion);
            Assert.AreEqual(expected.NpmNpmVersion, actual.NpmNpmVersion);
            Assert.AreEqual(expected.NpmMessage, actual.NpmMessage);
            Assert.AreEqual(expected.NpmArguments, actual.NpmArguments);
            Assert.AreEqual(expected.NpmCwd, actual.NpmCwd);
            Assert.AreEqual(expected.NpmVerbose, actual.NpmVerbose);
        }

        /// <summary>
        /// A test for FromInstall for older versions
        /// </summary>
        [TestMethod]
        public void FromInstallOld1Test()
        {
            IEnumerable<INpmInstalledPackage> actual;
            INpmSerialize target = new MockNpmFactory().GetSerialize("1.1.9");

            string output = MockTestData.InstallOld1Text();
            List<NpmInstalledPackage> expected = MockTestData.Install1Expected();

            actual = target.FromInstall(output);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count());
            int index = 0;
            foreach (NpmInstalledPackage actualItem in actual)
            {
                Assert.AreEqual(expected[index], actualItem, "item value differs");
                index++;
            }
        }
    }
}
