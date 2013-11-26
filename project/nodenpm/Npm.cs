using System.Collections.Generic;
using System.IO;
using System.Linq;
using npm_grunt;

namespace NodeNpm
{
    public static class NpmPackageSearch
    {
        public static IEnumerable<NpmApi> FindNpmApis(string workingDirectory)
        {
            const string fileToFind = "package.json";
            const string skipIn = "node_modules";
            var files = Directory.GetFiles(workingDirectory, fileToFind, SearchOption.AllDirectories).Where(a => !a.Contains(skipIn));

            foreach (var file in files)
            {
                var directory = Path.GetDirectoryName(file);
                yield return new NpmApi(new AutoNpmFactory(), directory, null);
            }
        }
    }
}