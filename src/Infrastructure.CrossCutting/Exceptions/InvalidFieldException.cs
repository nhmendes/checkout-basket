namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    using System;
    using System.Net;

    /// <summary>
    /// InvalidFieldException excpetion class
    /// </summary>
    [Serializable]
    public class InvalidFieldException : ApiExceptionBase
    {
        public InvalidFieldException()
           : base("No Results found.", (int)ErrorCode.InvalidFieldException, HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFieldException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidFieldException(string message)
            : base(message, (int)ErrorCode.InvalidFieldException, HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFieldException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidFieldException(string message, Exception innerException)
            : base(message, innerException, (int)ErrorCode.InvalidFieldException, HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFieldException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="developerMessage">The detailed error message.</param>
        public InvalidFieldException(string message, string developerMessage)
            : base(message, developerMessage, (int)ErrorCode.InvalidFieldException, HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFieldException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="developerMessage">The detailed error message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidFieldException(string message, string developerMessage, Exception innerException)
            : base(message, developerMessage, innerException, (int)ErrorCode.InvalidFieldException, HttpStatusCode.BadRequest)
        {
        }
    }
}
