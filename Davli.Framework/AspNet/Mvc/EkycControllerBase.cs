using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.AspNet.Mvc
{
    /// <summary>
    /// It's inherited from .net core base controller that has minimum member and properties. 
    /// So in implementing WebApi, we prefer that.
    /// </summary>
    public class DavliControllerBase : ControllerBase
    {
        //**************************************************************************************************
        //Variables:

        /// <summary>
        /// Logger.
        /// </summary>
        protected readonly ILogger _logger;
        //**************************************************************************************************
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        protected DavliControllerBase(ILogger logger)
        {
            _logger = logger;
        }
        //**************************************************************************************************
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        public DavliControllerBase()
        {

        }
        //**************************************************************************************************
    }
}
