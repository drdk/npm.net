// -----------------------------------------------------------------------
// <copyright file="MockNpmFactory.cs" company="Microsoft">
// Class for npm package manager unit tests
// </copyright>
// -----------------------------------------------------------------------

namespace NpmUnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NodeNpm;

    /// <summary>
    /// The class that acts as a class factory and returns a mock client for testing
    /// </summary>
    internal class MockNpmFactory : NpmFactory
    {
        /// <summary>
        /// Get MockNpmClient to support the version.
        /// </summary>
        /// <param name="version">npm version string or null for default</param>
        /// <returns>INpmClient instance</returns>
        public override INpmClient GetClient(string version)
        {
            return new MockNpmClient();
        }
    }
}
