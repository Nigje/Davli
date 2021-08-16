using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Exceptions
{
    public class DavliBusinessException:DavliException
    {
        public DavliBusinessException(string message = "BusinessException", string technicalMessage = "", string errorCode = null) : base(message, technicalMessage, errorCode)
        {
        }
        public DavliBusinessException(string message, string technicalMessage, Exception innerException, string errorCode = null) : base(message, technicalMessage, innerException, errorCode)
        {
        }
    }
}
