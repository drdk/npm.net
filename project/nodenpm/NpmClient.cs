// -----------------------------------------------------------------------
// <copyright file="NpmClient.cs" company="Microsoft">
// Class for npm package manager client execution
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Timers;

    /// <summary>
    /// The class that invokes NPM commands and returns the results text
    /// </summary>
    public class NpmClient : INpmClient
    {
        /// <summary>
        /// The default installation path for node
        /// </summary>
        private const string DefInstallPath = @"%ProgramFiles%\nodejs\";

        /// <summary>
        /// The default relative path to NPM
        /// </summary>
        private const string DefNpmRelativePath = @"node_modules\npm\bin\npm-cli.js";

        /// <summary>
        /// The executable file name for node
        /// </summary>
        private const string NodeExe = "node.exe";

        /// <summary>
        /// Error message
        /// </summary>
        private const string StartFailed = "Failed: process create - executable may already be running: ";

        /// <summary>
        /// Error message
        /// </summary>
        private const string NodeExeNotExistFormat = "The node.exe file is not found at {0}. Please check installation path.";

        /// <summary>
        /// Error message
        /// </summary>
        private const string NpmJSNotExistFormat = "The npm-cli.js file is not found at {0}. Please check installation.";

        /// <summary>
        /// Error message
        /// </summary>
        private const string StartWin32Exception = "Fatal: node process create failed. Verify inner exception message for details";

        /// <summary>
        /// Error message
        /// </summary>
        private const string WaitTimeout = "Timeout: waiting for process completion";

        /// <summary>
        /// Error message
        /// </summary>
        private const string WaitWin32Exception = "Fatal: process wait failed due to Win32 error";

        /// <summary>
        /// Error message
        /// </summary>
        private const string WaitSystemException = "Fatal: process wait failed due to system error";

        /// <summary>
        /// The output from most recent execute
        /// </summary>
        private string lastExecuteOutput;

        /// <summary>
        /// The error output from most recent execute
        /// </summary>
        private string lastExecuteErrorText;

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmClient" /> class.
        /// </summary>
        public NpmClient()
        {
            this.WorkingDirectory = Environment.CurrentDirectory;
            this.Timeout = 0;
            this.InstallPath = Environment.ExpandEnvironmentVariables(DefInstallPath);
            this.NpmRelativePath = DefNpmRelativePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmClient" /> class.
        /// Accepts the current project directory.
        /// </summary>
        /// <param name="wd">project directory</param>
        public NpmClient(string wd)
        {
            this.WorkingDirectory = wd;
            this.Timeout = 0;
            this.InstallPath = Environment.ExpandEnvironmentVariables(DefInstallPath);
            this.NpmRelativePath = DefNpmRelativePath;
        }

        /// <summary>
        /// Gets or sets installation path for node
        /// </summary>
        public string InstallPath 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets relative path to NPM from node installation
        /// </summary>
        public string NpmRelativePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets project directory used for some NPM commands
        /// </summary>
        public string WorkingDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets URL of remote registry. Only set if not using default NPM.
        /// </summary>
        public string Registry
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets HTTP Proxy URL
        /// </summary>
        public string HttpProxy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets HTTPS Proxy URL
        /// </summary>
        public string HttpsProxy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets timeout to use for NPM commands
        /// </summary>
        public int Timeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the output text from the last NPM command
        /// </summary>
        public string LastExecuteOutput
        {
            get
            {
                return this.lastExecuteOutput;
            }
        }

        /// <summary>
        /// Gets the error text from the last NPM command
        /// </summary>
        public string LastExecuteErrorText
        {
            get
            {
                return this.lastExecuteErrorText;
            }
        }

        /// <summary>
        /// Execute NPM command and save output
        /// </summary>
        /// <param name="cmd">command name</param>
        /// <param name="args">remainder of npm command line</param>
        /// <returns>exit code. 0 is success</returns>
        /// <remarks>LastExecuteOutput and LastExecuteError will be set</remarks>
        public int Execute(string cmd, string args)
        {
            this.lastExecuteErrorText = string.Empty;
            this.lastExecuteOutput = string.Empty;

            if (string.IsNullOrWhiteSpace(cmd))
            {
                throw new ArgumentNullException("cmd");
            }

            string nodepath = Path.Combine(this.InstallPath, NodeExe);
            if (!File.Exists(nodepath))
            {
                string error = string.Format(CultureInfo.InvariantCulture, NodeExeNotExistFormat, nodepath);
                throw new NpmException(error);
            }

            string npmcliPath = Path.Combine(this.InstallPath, this.NpmRelativePath);
            if (!File.Exists(npmcliPath))
            {
                string error = string.Format(CultureInfo.InvariantCulture, NpmJSNotExistFormat, npmcliPath);
                throw new NpmException(error);
            }

            // npm-cli.js full path in quotes with space trailer for command line
            string npmcli = "\"" + npmcliPath + "\" ";

            using (Process nodeNpm = new Process())
            {
                nodeNpm.StartInfo.RedirectStandardError = true;
                nodeNpm.StartInfo.RedirectStandardOutput = true;
                if (string.IsNullOrWhiteSpace(args))
                {
                    nodeNpm.StartInfo.Arguments = npmcli + cmd;
                }
                else
                {
                    nodeNpm.StartInfo.Arguments = npmcli + cmd + " " + args;
                }

                nodeNpm.StartInfo.FileName = nodepath;
                nodeNpm.StartInfo.UseShellExecute = false;
                nodeNpm.StartInfo.WorkingDirectory = this.WorkingDirectory;
                nodeNpm.StartInfo.CreateNoWindow = true;

                // It is not safe to read output and error synchronously
                NpmSync.AddNpmSync(nodeNpm);
                nodeNpm.OutputDataReceived += new DataReceivedEventHandler(StandardOutputHandler);
                nodeNpm.ErrorDataReceived += new DataReceivedEventHandler(ErrorOutputHandler);

                try
                {
                    bool started = nodeNpm.Start();
                    if (!started)
                    {
                        // node may already be running
                        throw new NpmException(StartFailed + nodeNpm.StartInfo.FileName);
                    }

                    nodeNpm.BeginOutputReadLine();
                    nodeNpm.BeginErrorReadLine();
                }
                catch (Win32Exception ex)
                {
                    NpmSync.RemNpmSync(nodeNpm);
                    throw new NpmException(StartWin32Exception, ex);
                }

                try
                {
                    bool exited;
                    if (this.Timeout == 0)
                    {
                        nodeNpm.WaitForExit();
                        exited = true;
                    }
                    else
                    {
                        exited = nodeNpm.WaitForExit(this.Timeout);
                        if (!exited)
                        {
                            nodeNpm.Kill();
                            throw new NpmException(WaitTimeout);
                        }

                        // need extra wait to ensure output flushed
                        nodeNpm.WaitForExit();
                    }
                }
                catch (Win32Exception ex)
                {
                    NpmSync.RemNpmSync(nodeNpm);
                    throw new NpmException(WaitWin32Exception, ex);
                }
                catch (SystemException ex)
                {
                    NpmSync.RemNpmSync(nodeNpm);
                    throw new NpmException(WaitSystemException, ex);
                }

                NpmSync sync = NpmSync.FindNpmSync(nodeNpm);
                NpmSync.RemNpmSync(nodeNpm);
                if (sync != null)
                {
                    this.lastExecuteOutput = sync.GetOutput();
                    this.lastExecuteErrorText = sync.GetError();
                }
                else
                {
                    this.lastExecuteOutput = string.Empty;
                    this.lastExecuteErrorText = string.Empty;
                }

                return nodeNpm.ExitCode;
            }
        }

        /// <summary>
        /// Delegate to receive stdout text lines
        /// </summary>
        /// <param name="sendingProcess">The sender</param>
        /// <param name="outLine">The event</param>
        private static void ErrorOutputHandler(
                                                object sendingProcess, 
                                                DataReceivedEventArgs outLine)
        {
            Process procObj = (Process)sendingProcess;
            if (procObj != null)
            {
                NpmSync sync = NpmSync.FindNpmSync(procObj);
                if (sync != null)
                {
                    sync.AddToError(outLine.Data);
                }
            }
        }

        /// <summary>
        /// Delegate to receive stderr text lines
        /// </summary>
        /// <param name="sendingProcess">The sender</param>
        /// <param name="outLine">The event</param>
        private static void StandardOutputHandler(
                                                object sendingProcess,
                                                DataReceivedEventArgs outLine)
        {
            Process procObj = (Process)sendingProcess;
            if (procObj != null)
            {
                NpmSync sync = NpmSync.FindNpmSync(procObj);
                if (sync != null)
                {
                    sync.AddToOuput(outLine.Data);
                }
            }
        }
    }
}
