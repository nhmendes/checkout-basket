namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Error class
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public Error(int code)
        {
            this.ErrorCode = code;
        }

        public Error(ErrorCode code)
        {
            this.ErrorCode = (int)code;
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the developer message.
        /// </summary>
        /// <value>
        /// The developer message.
        /// </value>
        public string DeveloperMessage { get; set; }

        /// <summary>
        /// Gets the more information.
        /// </summary>
        /// <value>
        /// The more information.
        /// </value>
        public string MoreInformation { get; private set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public System.Exception Exception { get; set; }

        /// <summary>
        /// Froms the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static Error[] FromException(System.Exception ex)
        {
            var errors = new List<Error>();

            if (ex != null)
            {
                if (ex is AggregateException)
                {
                    return FromAggregateException(ex as AggregateException);
                }
                else if (ex is ApiExceptionBase)
                {
                    errors.Add(FromBaseApiException(ex as ApiExceptionBase));
                }
                else if (ex is System.Exception)
                {
                    errors.Add(new Error(0)
                    {
                        DeveloperMessage = ex.Message,
                        Message = "Ups! Something went wrong.",
                        Exception = ex
                    });
                }
            }

            return errors.ToArray();
        }

        private static Error[] FromAggregateException(AggregateException ex)
        {
            List<Error> errors = new List<Error>();

            if (ex.InnerExceptions.Any())
            {
                foreach (System.Exception innerException in ex.InnerExceptions)
                {
                    Error[] innerErrors = FromException(innerException);
                    errors.AddRange(innerErrors);
                }
            }
            else
            {
                errors.Add(
                    new Error(0)
                    {
                        DeveloperMessage = ex.Message,
                        Exception = ex
                    });
            }

            return errors.ToArray();
        }

        private static Error FromBaseApiException(ApiExceptionBase ex)
        {
            Error error = new Error(ex.Code)
            {
                DeveloperMessage = ex.DeveloperMessage,
                Message = ex.Message
            };

            error.Exception = ex;

            return error;
        }
    }
}