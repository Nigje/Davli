using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Exceptions
{
    /// <summary>
    /// Indicates severity for a log. 
    /// Basically it corresnponds to log level but we introduced a new type to avoid dependency on a specific platform.
    /// </summary>
    public enum LogSeverityEnum
    {
        /// <summary>
        /// Debug.
        /// </summary>
        Debug,

        /// <summary>
        /// Info.
        /// </summary>
        Info,

        /// <summary>
        /// Warn.
        /// </summary>
        Warn,

        /// <summary>
        /// Error.
        /// </summary>
        Error,

        /// <summary>
        /// Critical.
        /// </summary>
        Critical
    }
}
