namespace Webmatrix_Npm
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
        /// Gets the reference type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Gets the reference URL
        /// </summary>
        string Url { get; }
    }
}
