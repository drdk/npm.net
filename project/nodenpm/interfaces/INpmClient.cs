// -----------------------------------------------------------------------
// <copyright file="INpmClient.cs" company="Microsoft">
// Interface for npm package manager client execution
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface to manage invoking NPM client
    /// </summary>
    public interface INpmClient
    {
        /// <summary>
        /// Gets or sets installation path for node
        /// </summary>
        string InstallPath { get; set; }

        /// <summary>
        /// Gets or sets relative path to NPM from node installation
        /// </summary>
        string NpmRelativePath { get; set; }

        /// <summary>
        /// Gets or sets project directory used for some NPM commands
        /// </summary>
        string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets URI of remote registry. Only set if not using default NPM.
        /// </summary>
        Uri Registry { get; set; }

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
        /// Gets the output text from the last NPM command
        /// </summary>
        string LastExecuteOutput { get; }

        /// <summary>
        /// Gets the error text from the last NPM command
        /// </summary>
        string LastExecuteErrorText { get; }

        /// <summary>
        /// Execute NPM command and save output
        /// </summary>
        /// <param name="cmd">command name</param>
        /// <param name="args">remainder of npm command line</param>
        /// <returns>exit code. 0 is success</returns>
        /// <remarks>LastExecuteOutput and LastExecuteError will be set</remarks>
        int Execute(string cmd, string args);
    }
}
