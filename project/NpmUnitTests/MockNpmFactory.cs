using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodejsNpm;

namespace NpmUnitTests
{
    class MockNpmFactory : NpmFactory
    {
        public override INpmClient GetClient(string version)
        {
            return new MockNpmClient();
        }

    }
}
