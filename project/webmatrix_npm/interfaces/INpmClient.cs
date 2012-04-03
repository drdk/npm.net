using System;
using System.Collections.Generic;
using System.Text;

namespace Webmatrix_Npm
{
    internal interface INpmClient
    {
        string InstallPath { get; set; }

        string NpmRelPath { get; set; }

        string WorkingDirectory { get; set; }

        string Registry { get; set; }

        string HttpProxy { get; set; }

        string HttpsProxy { get; set; }

        int Timeout { get; set; }

        int Execute(string cmd, string args, out string output, out string err);
    }
}
