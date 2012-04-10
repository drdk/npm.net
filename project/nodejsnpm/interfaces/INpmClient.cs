namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface to manage invoking NPM client
    /// </summary>
    internal interface INpmClient
    {
        /// <summary>
        /// Gets or sets installation path for node
        /// </summary>
        string InstallPath { get; set; }

        /// <summary>
        /// Gets or sets relative path to NPM from node installation
        /// </summary>
        string NpmRelPath { get; set; }

        /// <summary>
        /// Gets or sets project directory used for some NPM commands
        /// </summary>
        string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets URL of remote registry. Only set if not using default NPM.
        /// </summary>
        string Registry { get; set; }

        /// <summary>
        /// Gets or sets HTTP Proxy URL
        /// </summary>
        string HttpProxy { get; set; }

        /// <summary>
        /// Gets or sets HTTPS Proxy URL
        /// </summary>
        string HttpsProxy { get; set; }

        /// <summary>
        /// Gets or sets timeout to use for NPM commands
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Execute NPM command and return output
        /// </summary>
        /// <param name="cmd">command name</param>
        /// <param name="args">remainder of npm command line</param>
        /// <param name="output">captured stdout result from npm</param>
        /// <param name="err">captured stderr result from npm</param>
        /// <returns>exit code. 0 is success</returns>
        int Execute(string cmd, string args, out string output, out string err);
    }
}
