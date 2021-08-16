﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Exceptions
{
    public class DavliExceptionNotFound : DavliException
    {
        public DavliExceptionNotFound(string message = "Not Found.", string technicalMessage = "", string errorCode = null) : base(message, technicalMessage, errorCode)
        {
        }
        public DavliExceptionNotFound(string message , string technicalMessage , Exception innerException, string errorCode = null) : base(message, technicalMessage, innerException, errorCode)
        {
        }
    }
}