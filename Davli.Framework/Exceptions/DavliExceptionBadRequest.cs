using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Exceptions
{
    public class DavliExceptionBadRequest : DavliException
    {
        public DavliExceptionBadRequest(string message = "Bad Request.", string technicalMessage = "", string errorCode = null) : base(message, technicalMessage, errorCode)
        {
        }
        public DavliExceptionBadRequest(string message, string technicalMessage, Exception innerException, string errorCode = null) : base(message, technicalMessage, innerException, errorCode)
        {
        }
    }
}
