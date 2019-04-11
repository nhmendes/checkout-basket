namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    using System;
    using System.Net;

    public class UnsupportedOperationException : ApiExceptionBase
    {
        public UnsupportedOperationException()
            : base("Invalid operation.", (int)ErrorCode.NoResultsFoundException, HttpStatusCode.BadRequest)
        {
        }

        public UnsupportedOperationException(string message)
            : base(message, (int)ErrorCode.NoResultsFoundException, HttpStatusCode.BadRequest)
        {
        }

        public UnsupportedOperationException(string message, Exception exception) 
            : base(message, exception, HttpStatusCode.BadRequest)
        {
        }

        public UnsupportedOperationException(string message, string developerMessage)
            : base(message, developerMessage, (int)ErrorCode.NoResultsFoundException, HttpStatusCode.BadRequest)
        {
        }

        public UnsupportedOperationException(string message, string developerMessage, Exception innerException)
           : base(message, developerMessage, innerException, (int)ErrorCode.NoResultsFoundException, HttpStatusCode.BadRequest)
        {
        }
    }
}