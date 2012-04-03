using System;
using System.Collections.Generic;
using System.Text;

namespace Webmatrix_Npm
{
    /// <summary>
    /// Npm reference has a type and URL
    /// </summary>
    internal interface INpmReference
    {
        /// <summary>
        /// type string
        /// </summary>
        string Type { get; }

        /// <summary>
        /// URL of object
        /// </summary>
        string Url { get; }
    }
}
