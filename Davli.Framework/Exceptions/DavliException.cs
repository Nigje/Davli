using System;
using System.Net;

namespace Davli.Framework.Exceptions
{
    public class DavliException : Exception
    {
        //***********************************************************************************************
        //Variables:

        /// <summary>
        /// Error code that indicates a summary of error by using some words or numbers.
        /// </summary>
        public string ErrorCode { get; protected set; }

        /// <summary>
        /// Technical-details are not allowed to be shown to the user.
        /// Just log them or use them internally by software-technicians.
        /// </summary>
        public string TechnicalMessage { get; protected set; }

        /// <summary>
        /// Severity of the exception. The main usage will be for logs and monitoring.
        /// This way we can distinguish various exceptions in logs, 
        /// Think about the difference of between severity of a ValidationException and an Exception related to DB connection or Infrastructure. 
        /// Default: Error.
        /// </summary>
        public LogSeverityEnum Severity { get; protected set; }

        //***********************************************************************************************
        public DavliException(string message, string technicalMessage = "",string errorCode=null)
            : base(message)
        {
            TechnicalMessage = technicalMessage;
            Severity = LogSeverityEnum.Error;
            ErrorCode = errorCode;
        }
        //***********************************************************************************************
        public DavliException(string message, string technicalMessage, Exception innerException,string errorCode=null)
            : base(message, innerException)
        {
            TechnicalMessage = technicalMessage;
            Severity = LogSeverityEnum.Error;
            ErrorCode = errorCode;
        }
        //***********************************************************************************************
        public override string ToString()
        {
            string baseMessage = base.ToString();
            if (!string.IsNullOrEmpty(Message))
            {
                baseMessage = baseMessage.Replace(Message, $"{Message},\r\n TechnicalMessage: {TechnicalMessage}");
            }
            else
            {
                if (InnerException != null)
                {
                    int index = baseMessage.IndexOf("--->");
                    if (index >= 0)
                        baseMessage = baseMessage.Insert(index, $"\r\n TechnicalMessage: {TechnicalMessage}");
                }
                else
                {
                    baseMessage = baseMessage + $" \r\n TechnicalMessage: {TechnicalMessage}";
                }
            }

            return baseMessage;
        }
        //***********************************************************************************************
    }
}
