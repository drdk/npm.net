namespace NodejsNpm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Npm reference has a type and URL
    /// </summary>
    internal interface INpmReference
    {
        /// <summary>
        /// Gets or sets the reference type
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the reference URL
        /// </summary>
        string Url { get; set; }
    }
}
