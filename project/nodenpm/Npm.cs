using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using npm_grunt;

namespace NodeNpm
{
    public static class NpmPackageSearch
    {
        public static IEnumerable<NpmApi> FindNpmApis(string workingDirectory, string npmCacheDirectory)
        {
            const string fileToFind = "package.json";
            var skipIn = new[] { "node_modules", "bower_components" };
            var files = Directory.GetFiles(workingDirectory, fileToFind, SearchOption.AllDirectories).Where(a => !a.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Intersect(skipIn).Any());

            foreach (var file in files)
            {
                var directory = Path.GetDirectoryName(file);
                yield return new NpmApi(new AutoNpmFactory(npmCacheDirectory), directory, null);
            }
        }
    }
}