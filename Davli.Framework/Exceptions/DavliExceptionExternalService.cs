using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Exceptions
{
    public class DavliExceptionExternalService : DavliException
    {
        public HttpStatusCode HttpStatusCode { get; }
        public DavliExceptionExternalService(string message, HttpStatusCode httpStatusCode, string technicalMessage = "", string errorCode = null) : base(message, technicalMessage, errorCode)
        {
            HttpStatusCode = httpStatusCode;
        }
        public DavliExceptionExternalService(string message, HttpStatusCode httpStatusCode, string technicalMessage, Exception innerException, string errorCode = null) : base(message, technicalMessage, innerException, errorCode)
        {
            HttpStatusCode = httpStatusCode;
        }

    }
}
