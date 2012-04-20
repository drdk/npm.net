// -----------------------------------------------------------------------
// <copyright file="NpmException.cs" company="Microsoft">
// Npm Exception class
// </copyright>
// -----------------------------------------------------------------------

namespace NodeNpm
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom exception for npm errors
    /// </summary>
    [Serializable]
    public class NpmException : Exception, ISerializable
    {
        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmCode = "code ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmErrno = "errno ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmFile = "file ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmPath = "path ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmType = "type ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmSyscall = "syscall ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmSystem = "System ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmCommand = "command ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmNodeVersion = "node -v ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmNpmVersion = "npm -v ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmMessage = "message ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmArguments = "arguments ";

        /// <summary>
        /// Npm error property name
        /// </summary>
        private const string PrefixNpmCwd = "cwd ";

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmException" /> class.
        /// </summary>
        public NpmException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmException" /> class.
        /// </summary>
        /// <param name="message">Message for exception</param>
        public NpmException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmException" /> class.
        /// </summary>
        /// <param name="message">Message for exception</param>
        /// <param name="innerException">Initial exception wrapped by this exception</param>
        public NpmException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected NpmException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets or sets the Npm error code value
        /// </summary>
        public string NpmCode { get; set; }

        /// <summary>
        /// Gets or sets the Npm error errno value
        /// </summary>
        public string NpmErrno { get; set; }

        /// <summary>
        /// Gets or sets the Npm error file value
        /// </summary>
        public string NpmFile { get; set; }

        /// <summary>
        /// Gets or sets the Npm error path value
        /// </summary>
        public string NpmPath { get; set; }

        /// <summary>
        /// Gets or sets the Npm error type value
        /// </summary>
        public string NpmType { get; set; }

        /// <summary>
        /// Gets or sets the Npm error syscall value
        /// </summary>
        public string NpmSyscall { get; set; }

        /// <summary>
        /// Gets or sets the Npm error System value
        /// </summary>
        public string NpmSystem { get; set; }

        /// <summary>
        /// Gets or sets the Npm error code value
        /// </summary>
        public string NpmCommand { get; set; }

        /// <summary>
        /// Gets or sets the Npm error node version value
        /// </summary>
        public string NpmNodeVersion { get; set; }

        /// <summary>
        /// Gets or sets the Npm error npm version value
        /// </summary>
        public string NpmNpmVersion { get; set; }

        /// <summary>
        /// Gets or sets the Npm error message value
        /// </summary>
        public string NpmMessage { get; set; }

        /// <summary>
        /// Gets or sets the Npm error arguments value
        /// </summary>
        public string NpmArguments { get; set; }

        /// <summary>
        /// Gets or sets the Npm error cwd value
        /// </summary>
        public string NpmCwd { get; set; }

        /// <summary>
        /// Gets or sets the Npm error text messages
        /// </summary>
        public string NpmVerbose { get; set; }

        /// <summary>
        /// Converts a line of error output to an exception property
        /// or adds it to NpmVerbose
        /// </summary>
        /// <param name="text">Line of text</param>
        public void SetNpmValue(string text)
        {
            if (text == null)
            {
                return;
            }

            if (text.StartsWith(PrefixNpmCode, StringComparison.Ordinal))
            {
                this.NpmCode = text.Substring(PrefixNpmCode.Length);
            }
            else if (text.StartsWith(PrefixNpmErrno, StringComparison.Ordinal))
            {
                this.NpmErrno = text.Substring(PrefixNpmErrno.Length);
            }
            else if (text.StartsWith(PrefixNpmFile, StringComparison.Ordinal))
            {
                this.NpmFile = text.Substring(PrefixNpmFile.Length);
            }
            else if (text.StartsWith(PrefixNpmPath, StringComparison.Ordinal))
            {
                this.NpmPath = text.Substring(PrefixNpmPath.Length);
            }
            else if (text.StartsWith(PrefixNpmType, StringComparison.Ordinal))
            {
                this.NpmType = text.Substring(PrefixNpmType.Length);
            }
            else if (text.StartsWith(PrefixNpmSyscall, StringComparison.Ordinal))
            {
                this.NpmSyscall = text.Substring(PrefixNpmSyscall.Length);
            }
            else if (text.StartsWith(PrefixNpmSystem, StringComparison.Ordinal))
            {
                this.NpmSystem = text.Substring(PrefixNpmSystem.Length);
            }
            else if (text.StartsWith(PrefixNpmCommand, StringComparison.Ordinal))
            {
                this.NpmCommand = text.Substring(PrefixNpmCommand.Length);
            }
            else if (text.StartsWith(PrefixNpmNodeVersion, StringComparison.Ordinal))
            {
                this.NpmNodeVersion = text.Substring(PrefixNpmNodeVersion.Length);
            }
            else if (text.StartsWith(PrefixNpmNpmVersion, StringComparison.Ordinal))
            {
                this.NpmNpmVersion = text.Substring(PrefixNpmNpmVersion.Length);
            }
            else if (text.StartsWith(PrefixNpmMessage, StringComparison.Ordinal))
            {
                this.NpmMessage = text.Substring(PrefixNpmMessage.Length);
            }
            else if (text.StartsWith(PrefixNpmArguments, StringComparison.Ordinal))
            {
                this.NpmArguments = text.Substring(PrefixNpmArguments.Length);
            }
            else if (text.StartsWith(PrefixNpmCwd, StringComparison.Ordinal))
            {
                this.NpmCwd = text.Substring(PrefixNpmCwd.Length);
            }
            else
            {
                if (this.NpmVerbose == null)
                {
                    this.NpmVerbose = string.Empty;
                }

                // if starts with HTTP error status (3 digits >= 400)
                //  followed by a space, then skip over status code
                if (text.Length > 3)
                {
                    string status = text.Substring(0, 3);
                    int code;
                    if (int.TryParse(status, out code))
                    {
                        if (code >= 400)
                        {
                            string next = text.Substring(3, 1);
                            if (next == " ")
                            {
                                text = text.Substring(4);
                            }
                        }
                    }
                }

                this.NpmVerbose = this.NpmVerbose + text + "\n";
            }
        }

        /// <summary>
        /// implements get object data
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
