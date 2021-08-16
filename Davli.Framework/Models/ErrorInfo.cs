using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Models
{
    /// <summary>
    /// Api error result details.
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Error code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error details.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Source of the error. Generally it'll point to the service which the exception is originated from.
        /// </summary>
        public string Source { get; set; }
    }
}
