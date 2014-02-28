// -----------------------------------------------------------------------
// <copyright file="INpmClient.cs" company="Microsoft Open Technologies, Inc.">
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

        string NpmCacheDirectory { get; set; }

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
