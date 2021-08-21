using Davli.Framework.AspNet.Mvc;
using Davli.Framework.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Davli.Framework.SampleApi.Controllers
{
    /// <summary>
    /// Actions to throw exceptions.
    /// </summary>
    [Route("api/exception")]
    [ApiController]
    public class ExceptionController : DavliControllerBase
    {
        //********************************************************************************************************
        /// <summary>
        /// sample DavliExceptionBadRequest
        /// </summary>
        [HttpGet]
        [Route("bad-request")]
        public void BadRequest()
        {
            throw new DavliExceptionBadRequest("Sample BadRequest exception","Nothing","Bad_Request_Error");
        }
        //********************************************************************************************************
        /// <summary>
        /// sample DavliExceptionBadRequest
        /// </summary>
        [HttpGet]
        [Route("business")]
        public void BusinessException()
        {
            throw new DavliBusinessException("Sample Business exception", "Nothing",new DavliException("Samle inner exception"));
        }
        //********************************************************************************************************
        /// <summary>
        /// sample DavliExceptionExternalService
        /// </summary>
        [HttpGet]
        [Route("external-service-exception")]
        public void ExternalServiceException()
        {
            throw new DavliExceptionExternalService("Sample ExternalServiceException exception.", HttpStatusCode.RequestTimeout);
        }
        //********************************************************************************************************
    }
}
