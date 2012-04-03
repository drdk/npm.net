using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Text;
using System.Timers;
using System.IO;

namespace Webmatrix_Npm
{
    internal class NpmClient : INpmClient
    {
        // Path and file names
        private const string DefInstallPath = @"%ProgramFiles%\nodejs\";
        private const string DefNpmRelPath = @"node_modules\npm\bin\npm-cli.js";
        private const string NodeExe = "node.exe";

        // strings that need localization
        private const string StartFailed = "Failed: process create - executable may already be running: ";
        private const string StartWin32Exception = "Fatal: process create failed due to Win32 error";
        private const string WaitTimeout = "Timeout: waiting for process completion";
        private const string WaitWin32Exception = "Fatal: process wait failed due to Win32 error";
        private const string WaitSystemException = "Fatal: process wait failed due to system error";

        public NpmClient()
        {
            WorkingDirectory = Environment.CurrentDirectory;
            Timeout = 0;
            InstallPath = Environment.ExpandEnvironmentVariables(DefInstallPath);
            NpmRelPath = DefNpmRelPath;
        }

        public NpmClient(string cwd)
        {
            WorkingDirectory = cwd;
            Timeout = 0;
            InstallPath = Environment.ExpandEnvironmentVariables(DefInstallPath);
            NpmRelPath = DefNpmRelPath;
        }

        public string InstallPath { get; set; }
        public string NpmRelPath { get; set; }
        public string WorkingDirectory { get; set; }
        public string Registry { get; set; }
        public string HttpProxy { get; set; }
        public string HttpsProxy { get; set; }
        public int Timeout { get; set; }

        public virtual int Execute(string cmd, string args, out string output, out string err)
        {
            if (String.IsNullOrWhiteSpace(cmd))
            {
                throw new ArgumentNullException("cmd");
            }

            // npm-cli.js full path in quotes with space trailer
            string npmcli = "\"" + Path.Combine(InstallPath, NpmRelPath) + "\" ";

            using (Process nodeNpm = new Process())
            {
                nodeNpm.StartInfo.RedirectStandardError = true;
                nodeNpm.StartInfo.RedirectStandardOutput = true;
                if (String.IsNullOrWhiteSpace(args))
                {
                    nodeNpm.StartInfo.Arguments = npmcli + cmd;
                }
                else
                {
                    nodeNpm.StartInfo.Arguments = npmcli + cmd + " " + args;
                }
                nodeNpm.StartInfo.FileName = Path.Combine(InstallPath, NodeExe);
                nodeNpm.StartInfo.UseShellExecute = false;
                nodeNpm.StartInfo.WorkingDirectory = WorkingDirectory;
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
                        throw new ApplicationException(StartFailed + nodeNpm.StartInfo.FileName);
                    }

                    nodeNpm.BeginOutputReadLine();
                    nodeNpm.BeginErrorReadLine();
                }
                catch (Win32Exception ex)
                {
                    NpmSync.RemNpmSync(nodeNpm);
                    throw new ApplicationException(StartWin32Exception, ex);
                }

                try
                {
                    bool exited;
                    if (Timeout == 0)
                    {
                        nodeNpm.WaitForExit();
                        exited = true;
                    }
                    else
                    {
                        exited = nodeNpm.WaitForExit(Timeout);
                        if (!exited)
                        {
                            nodeNpm.Kill();
                            throw new ApplicationException(WaitTimeout);
                        }
                        // need extra wait to ensure output flushed
                        nodeNpm.WaitForExit();
                    }
                }
                catch (Win32Exception ex)
                {
                    NpmSync.RemNpmSync(nodeNpm);
                    throw new ApplicationException(WaitWin32Exception, ex);
                }
                catch (SystemException ex)
                {
                    NpmSync.RemNpmSync(nodeNpm);
                    throw new ApplicationException(WaitSystemException, ex);
                }

                NpmSync sync = NpmSync.FindNpmSync(nodeNpm);
                NpmSync.RemNpmSync(nodeNpm);
                output = "";
                err = "";
                if (sync != null)
                {
                    output = sync.GetOutput();
                    err = sync.GetError();
                }
                return nodeNpm.ExitCode;
            }
        }

        private static void ErrorOutputHandler(object sendingProcess, 
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

        private static void StandardOutputHandler(object sendingProcess,
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

    internal class NpmSync
    {
        internal static List<NpmSync> RunningNpms;

        private NpmSync(Process npm)
        {
            nodeNpm = npm;
            sbOutput = null;
            sbError = null;
        }

        internal void AddToOuput(string line)
        {
            if (sbOutput == null)
            {
                sbOutput = new StringBuilder();
            }
            sbOutput.AppendLine(line);
        }

        internal void AddToError(string line)
        {
            if (sbError == null)
            {
                sbError = new StringBuilder();
            }
            sbError.AppendLine(line);
        }

        internal string GetOutput()
        {
            if (sbOutput == null) return "";
            return sbOutput.ToString();
        }

        internal string GetError()
        {
            if (sbError == null) return "";
            return sbError.ToString();
        }

        internal static NpmSync FindNpmSync(Process obj)
        {
            if (RunningNpms == null)
            {
                return null;
            }
            foreach (NpmSync sync in RunningNpms)
            {
                if (sync.nodeNpm == obj)
                    return sync;
            }
            return null;
        }

        internal static void AddNpmSync(Process obj)
        {
            if (RunningNpms == null)
            {
                RunningNpms = new List<NpmSync>();
            }
            NpmSync sync = new NpmSync(obj);
            RunningNpms.Add(sync);
        }

        internal static void RemNpmSync(Process obj)
        {
            if (RunningNpms == null)
            {
                return;
            }
            NpmSync sync = FindNpmSync(obj);
            if (sync != null)
            {
                RunningNpms.Remove(sync);
            }
        }

        private Process nodeNpm;
        private StringBuilder sbOutput;
        private StringBuilder sbError;
    }
}
