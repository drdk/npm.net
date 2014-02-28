using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNpm;

namespace npm_grunt
{
    public class AutoNpmFactory : NpmFactory
    {
        private readonly string _npmCacheDirectory;

        public AutoNpmFactory(string npmCacheDirectory)
        {
            _npmCacheDirectory = npmCacheDirectory;
        }

        public override INpmClient GetClient(string version)
        {
            var npmExecutable = FindExePath("node.exe");
            var path = Path.GetDirectoryName(npmExecutable);
            return new NpmClient()
                {
                    InstallPath = path,
                    NpmCacheDirectory = _npmCacheDirectory
                };
        }

        private string FindExePath(string exe)
        {
            exe = Environment.ExpandEnvironmentVariables(exe);
            if (!File.Exists(exe))
            {
                if (Path.GetDirectoryName(exe) == String.Empty)
                {
                    foreach (var test in (Environment.GetEnvironmentVariable("PATH") ?? "").Split(';'))
                    {
                        var path = test.Trim();
                        if (!String.IsNullOrEmpty(path) && File.Exists(path = Path.Combine(path, exe)))
                            return Path.GetFullPath(path);
                    }
                }
                throw new FileNotFoundException(new FileNotFoundException().Message, exe);
            }
            return Path.GetFullPath(exe);
        }
    }
}
