namespace NpmUnitTests
{
    using NodejsNpm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    ///This is a test class for SemVerTest and is intended
    ///to contain all SemVerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SemVerTest
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
        ///A test for SemVer Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void SemVerConstructorTest()
        {
            int major = 1;
            int minor = 2;
            int patch = 3;
            bool isBuild = false;
            string buildOrPrerel = "prerel";
            SemVer target = new SemVer(major, minor, patch, isBuild, buildOrPrerel);
            Assert.IsNotNull(target);
            Assert.AreEqual(major, target.Major);
            Assert.AreEqual(minor, target.Minor);
            Assert.AreEqual(patch, target.Patch);
            Assert.AreEqual(buildOrPrerel, target.PreRelease);
            Assert.IsNull(target.Build);
            Assert.AreEqual("1.2.3-prerel", target.Version);
        }

        /// <summary>
        ///A test for SemVer Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void SemVerConstructorTestBuild()
        {
            int major = 2;
            int minor = 0;
            int patch = 7;
            bool isBuild = true;
            string buildOrPrerel = "27";
            SemVer target = new SemVer(major, minor, patch, isBuild, buildOrPrerel);
            Assert.IsNotNull(target);
            Assert.AreEqual(major, target.Major);
            Assert.AreEqual(minor, target.Minor);
            Assert.AreEqual(patch, target.Patch);
            Assert.AreEqual(buildOrPrerel, target.Build);
            Assert.IsNull(target.PreRelease);
            Assert.AreEqual("2.0.7+27", target.Version);
        }

        /// <summary>
        ///A test for SemVer Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void SemVerConstructorTest1()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            Assert.IsNotNull(target);
            Assert.AreEqual(major, target.Major);
            Assert.AreEqual(minor, target.Minor);
            Assert.AreEqual(patch, target.Patch);
            Assert.IsNull(target.PreRelease);
            Assert.IsNull(target.Build);
            Assert.AreEqual("2.1.3", target.Version);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void CompareToTestEqual()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            SemVer semver = new SemVer(major, minor, patch);
            int expected = 0;
            int actual;
            actual = target.CompareTo(semver);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void CompareToTestGreater()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            major = 2;
            minor = 1;
            patch = 2;
            SemVer semver = new SemVer(major, minor, patch);
            int expected = 1;
            int actual = target.CompareTo(semver);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void CompareToTestLess()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            major = 2;
            minor = 1;
            patch = 4;
            SemVer semver = new SemVer(major, minor, patch);
            int expected = -1;
            int actual = target.CompareTo(semver);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void CompareToTestGreaterMinor()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            major = 2;
            minor = 0;
            patch = 5;
            SemVer semver = new SemVer(major, minor, patch);
            int expected = 1;
            int actual = target.CompareTo(semver);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void CompareToTestLessMinor()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            major = 2;
            minor = 2;
            patch = 1;
            SemVer semver = new SemVer(major, minor, patch);
            int expected = -1;
            int actual = target.CompareTo(semver);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void CompareToTestGreaterMajor()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            major = 1;
            minor = 7;
            patch = 5;
            SemVer semver = new SemVer(major, minor, patch);
            int expected = 1;
            int actual = target.CompareTo(semver);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void CompareToTestLessMajor()
        {
            int major = 2;
            int minor = 1;
            int patch = 3;
            SemVer target = new SemVer(major, minor, patch);
            major = 3;
            minor = 0;
            patch = 1;
            SemVer semver = new SemVer(major, minor, patch);
            int expected = -1;
            int actual = target.CompareTo(semver);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void ParseTest()
        {
            string version = "1.2.3";
            SemVer actual;
            actual = SemVer.Parse(version);
            Assert.IsInstanceOfType(actual, typeof(SemVer));
            int expmajor = 1;
            int expMinor = 2;
            int expPatch = 3;
            Assert.AreEqual(expmajor, actual.Major);
            Assert.AreEqual(expMinor, actual.Minor);
            Assert.AreEqual(expPatch, actual.Patch);
        }

        /// <summary>
        ///A test for ParseHelper
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void ParseHelperTest()
        {
            string version = "1.2.3";
            SemVer_Accessor semver = null;
            int expected = 0;
            int actual;
            actual = SemVer_Accessor.ParseHelper(version, out semver);
            Assert.AreEqual(expected, actual);
            int expmajor = 1;
            int expMinor = 2;
            int expPatch = 3;
            Assert.AreEqual(expmajor, semver.Major);
            Assert.AreEqual(expMinor, semver.Minor);
            Assert.AreEqual(expPatch, semver.Patch);
        }

        /// <summary>
        ///A test for ParseHelper
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void ParseHelperTestNoPatch()
        {
            string version = "1.2";
            SemVer_Accessor semver = null;
            int expected = 0;
            int actual;
            actual = SemVer_Accessor.ParseHelper(version, out semver);
            Assert.AreEqual(expected, actual);
            int expmajor = 1;
            int expMinor = 2;
            int expPatch = 0;
            Assert.AreEqual(expmajor, semver.Major);
            Assert.AreEqual(expMinor, semver.Minor);
            Assert.AreEqual(expPatch, semver.Patch);
        }

        /// <summary>
        ///A test for ParseHelper
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void ParseHelperTestNoMinor()
        {
            string version = "1";
            SemVer_Accessor semver = null;
            int expected = 0;
            int actual;
            actual = SemVer_Accessor.ParseHelper(version, out semver);
            Assert.AreEqual(expected, actual);
            int expmajor = 1;
            int expMinor = 0;
            int expPatch = 0;
            Assert.AreEqual(expmajor, semver.Major);
            Assert.AreEqual(expMinor, semver.Minor);
            Assert.AreEqual(expPatch, semver.Patch);
        }

        /// <summary>
        ///A test for ParseHelper
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void ParseHelperTestPrerel()
        {
            string version = "1.2.3-prerel.1.2";
            SemVer_Accessor semver = null;
            int expected = 0;
            int actual;
            actual = SemVer_Accessor.ParseHelper(version, out semver);
            Assert.AreEqual(expected, actual);
            int expmajor = 1;
            int expMinor = 2;
            int expPatch = 3;
            string expPreRelease = "prerel.1.2";
            Assert.AreEqual(expmajor, semver.Major);
            Assert.AreEqual(expMinor, semver.Minor);
            Assert.AreEqual(expPatch, semver.Patch);
            Assert.AreEqual(expPreRelease, semver.PreRelease);
        }

        /// <summary>
        ///A test for ParseHelper
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void ParseHelperTestBuild()
        {
            string version = "1.2.3+build-5";
            SemVer_Accessor semver = null;
            int expected = 0;
            int actual;
            actual = SemVer_Accessor.ParseHelper(version, out semver);
            Assert.AreEqual(expected, actual);
            int expmajor = 1;
            int expMinor = 2;
            int expPatch = 3;
            string expBuild = "build-5";
            Assert.AreEqual(expmajor, semver.Major);
            Assert.AreEqual(expMinor, semver.Minor);
            Assert.AreEqual(expPatch, semver.Patch);
            Assert.AreEqual(expBuild, semver.Build);
        }

        /// <summary>
        ///A test for TryParse
        ///</summary>
        [TestMethod()]
        public void TryParseTest()
        {
            string version = "1.2.3";
            SemVer semver = null;
            bool expected = true;
            bool actual;
            actual = SemVer.TryParse(version, out semver);
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(semver);
            int expmajor = 1;
            int expMinor = 2;
            int expPatch = 3;
            Assert.AreEqual(expmajor, semver.Major);
            Assert.AreEqual(expMinor, semver.Minor);
            Assert.AreEqual(expPatch, semver.Patch);
        }

        /// <summary>
        ///A test for Build
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void BuildTest()
        {
            SemVer_Accessor target = new SemVer_Accessor(0, 0, 0);
            string expected = "build.1.2.3";
            string actual;
            target.Build = expected;
            actual = target.Build;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Major
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void MajorTest()
        {
            SemVer_Accessor target = new SemVer_Accessor(0, 0, 0);
            int expected = 4;
            int actual;
            target.Major = expected;
            actual = target.Major;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Minor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void MinorTest()
        {
            SemVer_Accessor target = new SemVer_Accessor(0, 0, 0);
            int expected = 27;
            int actual;
            target.Minor = expected;
            actual = target.Minor;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Patch
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void PatchTest()
        {
            SemVer_Accessor target = new SemVer_Accessor(0, 0, 0);
            int expected = 13;
            int actual;
            target.Patch = expected;
            actual = target.Patch;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PreRelease
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void PreReleaseTest()
        {
            SemVer_Accessor target = new SemVer_Accessor(0, 0, 0);
            string expected = "1.2.3";
            string actual;
            target.PreRelease = expected;
            actual = target.PreRelease;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Version
        ///</summary>
        [TestMethod()]
        [DeploymentItem("nodejsnpm.dll")]
        public void VersionTest()
        {
            SemVer_Accessor target = new SemVer_Accessor(1, 2, 3);
            string expected = "1.2.3";
            string actual;
            target.Version = expected;
            actual = target.Version;
            Assert.AreEqual(expected, actual);
        }
    }
}
