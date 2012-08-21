// -----------------------------------------------------------------------
// <copyright file="NpmSync.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Object used to synchronize client and delegate output handlers
    /// </summary>
    internal class NpmSync
    {
        /// <summary>
        /// List of active NPM requests
        /// </summary>
        private static List<NpmSync> runningNpms;

        /// <summary>
        /// The process object
        /// </summary>
        private Process nodeNpm;

        /// <summary>
        /// The output text string builder
        /// </summary>
        private StringBuilder output;

        /// <summary>
        /// The error text string builder
        /// </summary>
        private StringBuilder errorOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmSync" /> class.
        /// </summary>
        /// <param name="npm">Process object</param>
        private NpmSync(Process npm)
        {
            this.nodeNpm = npm;
            this.output = null;
            this.errorOutput = null;
        }

        /// <summary>
        /// Get NpmSync for Process object
        /// </summary>
        /// <param name="obj">Process object</param>
        /// <returns>matching NpmSync</returns>
        internal static NpmSync FindNpmSync(Process obj)
        {
            if (runningNpms == null)
            {
                return null;
            }

            foreach (NpmSync sync in runningNpms)
            {
                if (sync.nodeNpm == obj)
                {
                    return sync;
                }
            }

            return null;
        }

        /// <summary>
        /// Create new NpmSync and associate it with this Porcess object
        /// </summary>
        /// <param name="obj">Process object</param>
        /// <returns>NpmSync to track this request</returns>
        internal static NpmSync AddNpmSync(Process obj)
        {
            if (runningNpms == null)
            {
                runningNpms = new List<NpmSync>();
            }

            NpmSync sync = new NpmSync(obj);
            runningNpms.Add(sync);
            return sync;
        }

        /// <summary>
        /// Remove the NpmSync object
        /// </summary>
        /// <param name="sync">sync object</param>
        internal static void RemNpmSync(NpmSync sync)
        {
            if (sync != null)
            {
                runningNpms.Remove(sync);
            }
        }

        /// <summary>
        /// Remove the NpmSync for the specified Process object
        /// </summary>
        /// <param name="obj">Process object</param>
        internal static void RemNpmSync(Process obj)
        {
            if (runningNpms == null)
            {
                return;
            }

            NpmSync sync = FindNpmSync(obj);
            if (sync != null)
            {
                runningNpms.Remove(sync);
            }
        }

        /// <summary>
        /// Appends line to output
        /// </summary>
        /// <param name="line">Line of text</param>
        internal void AddToOuput(string line)
        {
            if (this.output == null)
            {
                this.output = new StringBuilder();
            }

            this.output.AppendLine(line);
        }

        /// <summary>
        /// Appends line to error
        /// </summary>
        /// <param name="line">Line of text</param>
        internal void AddToError(string line)
        {
            if (this.errorOutput == null)
            {
                this.errorOutput = new StringBuilder();
            }

            this.errorOutput.AppendLine(line);
        }

        /// <summary>
        /// Retrieve output text
        /// </summary>
        /// <returns>output as string</returns>
        internal string GetOutput()
        {
            if (this.output == null)
            {
                return string.Empty;
            }

            return this.output.ToString();
        }

        /// <summary>
        /// Retrieve error text
        /// </summary>
        /// <returns>error as string</returns>
        internal string GetError()
        {
            if (this.errorOutput == null)
            {
                return string.Empty;
            }

            return this.errorOutput.ToString();
        }
    }
}
