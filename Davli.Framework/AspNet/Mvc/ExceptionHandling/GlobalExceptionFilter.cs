using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Davli.Framework.DI;
using Davli.Framework.Exceptions;
using Davli.Framework.Models;
using Davli.Framework.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.AspNet.Mvc.ExceptionHandling
{
    /// <summary>
    /// It manipulates all exceptions that are thrown.
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter, ITransientLifetime
    {
        //************************************************************************************************
        //Variables:
        /// <summary>
        /// Define logger to log exceptions.
        /// </summary>
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IWebHostEnvironment _environment;
        //************************************************************************************************

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }
        //************************************************************************************************
        /// <summary>
        /// On exception event.
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            LogException(context.Exception);
            HandleResponse(context);
        }
        //************************************************************************************************
        /// <summary>
        /// Log exception.
        /// </summary>
        /// <param name="exception"></param>
        private void LogException(Exception exception)
        {
            var defaultSeverity = LogSeverityEnum.Error;
            string exceptionMessage = $"Processed an unhandled exception of type { exception.GetType().Name }:\r\nMessage: { exception.Message }";
            if (exception is DavliException businessException)
            {
                defaultSeverity = businessException.Severity;
                exceptionMessage = businessException.ToString();
            }
            switch (defaultSeverity)
            {
                case LogSeverityEnum.Debug:
                    _logger.LogDebug(exception, exceptionMessage);
                    break;
                case LogSeverityEnum.Info:
                    _logger.LogInformation(exception, exceptionMessage);
                    break;
                case LogSeverityEnum.Warn:
                    _logger.LogWarning(exception, exceptionMessage);
                    break;
                case LogSeverityEnum.Error:
                    _logger.LogError(exception, exceptionMessage);
                    break;
                case LogSeverityEnum.Critical:
                    _logger.LogCritical(exception, exceptionMessage);
                    break;
                default:
                    _logger.LogWarning(
                        "Invalid severity type is passed to LogException method, Please check the code and correct the issue");
                    _logger.LogError(exception, exceptionMessage);
                    break;
            }
        }
        //************************************************************************************************
        /// <summary>
        /// Handle resposen.
        /// </summary>
        /// <param name="context"></param>
        private void HandleResponse(ExceptionContext context)
        {
            //We use the error code that is set in BusinessException and 
            var errorCode = ((context.Exception as DavliException)?.ErrorCode);

            context.HttpContext.Response.StatusCode = (int)GetHttpStatusCode(context.Exception);

            context.ExceptionHandled = true;
            //Todo: handle exceptions with more policy and restrictions.
            var exceptionApiResult = TryGetExceptionApiResult(context.Exception);
            if (exceptionApiResult == null)
            {
                context.Result = new ObjectResult(new ExceptionApiResult
                {
                    __unauthorizedRequest = !context.HttpContext.User.Identity.IsAuthenticated,
                    __traceId = "",//Todo: Implement
                    Error = new ErrorInfo
                    {
                        Details = GetDetails(context.Exception),
                        ErrorCode = errorCode,
                        Message = context.Exception.Message,
                        Source = OptionService.GetOption<ServiceInfoOption>().FullName
                    }
                });
            }
            else
            {
                exceptionApiResult.__unauthorizedRequest = !context.HttpContext.User.Identity.IsAuthenticated;
                context.Result = new ObjectResult(exceptionApiResult);
            }

        }
        //************************************************************************************************
        private string GetDetails(Exception exception)
        {
            if (_environment.IsProduction())
                return null;
            string details = "";
            Exception tempException = exception;
            while (tempException != null)
            {
                details = tempException.GetType().Name + ": " + tempException.Message;
                if (tempException is DavliException)
                {
                    details += Environment.NewLine + ((DavliException)tempException).TechnicalMessage;
                }
                //Exception StackTrace
                if (!string.IsNullOrEmpty(tempException.StackTrace))
                {
                    details += Environment.NewLine + "Stack Trace: " + tempException.StackTrace + Environment.NewLine + Environment.NewLine;
                }
                tempException = tempException.InnerException;
            }
            return details;
        }
        //************************************************************************************************
        /// <summary>
        /// Get HttpStatusCode by using exception type.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private HttpStatusCode GetHttpStatusCode(Exception exception)
        {
            if (exception is DavliExceptionNotFound)
                return HttpStatusCode.NotFound;
            else if (exception is DavliExceptionInvalidParameter)
                return HttpStatusCode.UnprocessableEntity;
            else if (exception is DavliBusinessException)
                return HttpStatusCode.BadRequest;
            else if (exception is DavliExceptionBadRequest)
                return HttpStatusCode.BadRequest;
            else if (exception is DavliExceptionExternalService)
                return ((DavliExceptionExternalService)exception).HttpStatusCode;
            else if (exception is DavliException)
            {
                //If there is HttpStatusCode property in exception, return its value.
                var property = exception.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == typeof(HttpStatusCode));
                if (property != null)
                {
                    HttpStatusCode httpStatusCode = (HttpStatusCode)property.GetValue(exception, null);
                    return httpStatusCode;
                }
            }
            return HttpStatusCode.InternalServerError;
        }
        //************************************************************************************************
        /// <summary>
        /// Check the exception message and if it's generated in another internal service, it would return.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private ExceptionApiResult TryGetExceptionApiResult(Exception exception)
        {
            //todo: Handle internal api call exception with specific  Exception type in swagger api.
            ExceptionApiResult exceptionApi = null;
            string apiExceptionMessage = null;
            try
            {
                var startingExceptionContentIndex = exception.Message.IndexOf("{");
                var endExceptionContentIndex = exception.Message.LastIndexOf("}");

                apiExceptionMessage = exception.Message.Substring(startingExceptionContentIndex, endExceptionContentIndex - startingExceptionContentIndex + 1);
                while (!string.IsNullOrEmpty(apiExceptionMessage))
                {
                    ExceptionApiResult deserializedApiException = JsonConvert.DeserializeObject<ExceptionApiResult>(apiExceptionMessage);


                    exceptionApi = new ExceptionApiResult();
                    exceptionApi.Error = new ErrorInfo();
                    exceptionApi.__traceId = deserializedApiException.__traceId;
                    exceptionApi.__wrapped = deserializedApiException.__wrapped;
                    exceptionApi.__unauthorizedRequest = deserializedApiException.__unauthorizedRequest;

                    exceptionApi.Error.ErrorCode = deserializedApiException.Error.ErrorCode;
                    exceptionApi.Error.Message = deserializedApiException.Error.Message;
                    exceptionApi.Error.Source = deserializedApiException.Error.Source;
                    exceptionApi.Error.Details = deserializedApiException.Error.Details;
                    apiExceptionMessage = deserializedApiException.Error.Message;
                }
            }
            catch (Exception ex)
            {

            }
            return exceptionApi;
        }

        //************************************************************************************************
    }
}
