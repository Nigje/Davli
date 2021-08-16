using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Models
{
    /// <summary>
    /// Api response template for errors.
    /// </summary>
    public class ExceptionApiResult
    {
        /// <summary>
        /// This property can be used to indicate that the current user has no privilege to perform this request.
        /// </summary>
        public bool __unauthorizedRequest { get; set; }
        /// <summary>
        /// It's used in the client to detect if this is a response wrapped by YKYC framework.
        /// </summary>
        public bool __wrapped { get; set; } = true;

        /// <summary>
        /// If set, represents the traceId of the current request.
        /// For now, the traceId is exposed only in Development and Staging environment.
        /// </summary>
        public string __traceId { get; set; }

        /// <summary>
        /// Error details (Must and only set if <see cref="Success"/> is false).
        /// </summary>
        public ErrorInfo Error { get; set; }
    }
}
