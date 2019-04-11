namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// This class represents the base Exception for all the exception that are designed in the API Solution.
    /// </summary>
    [Serializable]
    public abstract class ApiExceptionBase
        : Exception
    {
        /// <summary>
        /// Creates an instance of the BaseApiException
        /// </summary>
        protected ApiExceptionBase()
            : base()
        {
            this.Initialize();
        }

        /// <summary>
        /// Creates an instance of the BaseApiException and a message must be passed.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        protected ApiExceptionBase(string message, HttpStatusCode httpCode)
            : base(message)
        {           
            this.Initialize();

            this.HttpCode = httpCode;
        }

        /// <summary>
        /// Creates an instance of the BaseApiException and a message must be passed.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        protected ApiExceptionBase(string message, int code, HttpStatusCode httpCode)
            : base(message)
        {
            this.Initialize();

            this.HttpCode = httpCode;
            this.Code = code;
        }

        /// <summary>
        /// Creates an instance of the BaseApiException and a message must be passed.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <param name="innerException">The exception to be added as an inner exception.</param>
        protected ApiExceptionBase(string message, Exception inner, HttpStatusCode httpCode)
            : base(message, inner)
        {
            this.Initialize();

            this.HttpCode = httpCode;
        }

        /// <summary>
        /// Creates an instance of the BaseApiException and a message must be passed.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <param name="innerException">The exception to be added as an inner exception.</param>
        protected ApiExceptionBase(string message, Exception inner, int code, HttpStatusCode httpCode)
            : base(message, inner)
        {
            this.Initialize();

            this.HttpCode = httpCode;
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionBase" /> class.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <param name="developerMessage">The detailed error message.</param>
        protected ApiExceptionBase(string message, string developerMessage, HttpStatusCode httpCode)
                   : base(message)
        {
            this.Initialize();

            this.HttpCode = httpCode;
            this.DeveloperMessage = developerMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionBase" /> class.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <param name="developerMessage">The detailed error message.</param>
        /// <param name="code">The code.</param>
        protected ApiExceptionBase(string message, string developerMessage, int code, HttpStatusCode httpCode)
                    : base(message)
        {
            this.Initialize();

            this.Code = code;
            this.HttpCode = httpCode;
            this.DeveloperMessage = developerMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionBase" /> class.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <param name="developerMessage">The detailed error message.</param>
        /// <param name="innerException">The exception to be added as an inner exception.</param>
        protected ApiExceptionBase(string message, string developerMessage, Exception inner, HttpStatusCode httpCode)
                    : base(message, inner)
        {
            this.Initialize();

            this.HttpCode = httpCode;
            this.DeveloperMessage = developerMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionBase" /> class.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <param name="developerMessage">The detailed error message.</param>
        /// <param name="innerException">The exception to be added as an inner exception.</param>
        /// <param name="code">The code.</param>
        protected ApiExceptionBase(string message, string developerMessage, Exception inner, int code, HttpStatusCode httpCode)
                    : base(message, inner)
        {
            this.Initialize();

            this.Code = code;
            this.HttpCode = httpCode;
            this.DeveloperMessage = developerMessage;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected ApiExceptionBase(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Action = (HandleAction)info.GetValue("Action", typeof(HandleAction));
            this.HttpCode = (HttpStatusCode)info.GetValue("HttpCode", typeof(HttpStatusCode));
            this.Handled = info.GetBoolean("Handled");
        }

        /// <summary>
        /// Gets or sets whether this exception was already handled.
        /// </summary>
        /// <remarks>
        /// When an <see cref="ApiExceptionBase"/> is handled, it means that the specified HandleAction was executed.
        /// </remarks>
        public bool Handled { get; internal set; }

        /// <summary>
        /// Gets or sets the action that will be executed by <see cref="ExceptionHandler" for this exception./>
        /// </summary>
        public HandleAction Action { get; protected set; }

        /// <summary>
        /// Gets or sets the ErrorCode/>
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// Gets or sets the HttpStatusCode that the Api must return
        /// </summary>
        public HttpStatusCode HttpCode { get; protected set; }

        /// <summary>
        /// Gets or sets the developer message for better debugging purposes
        /// </summary>
        public string DeveloperMessage { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Handled", this.Handled);
            info.AddValue("Action", this.Action);
            info.AddValue("HttpCode", this.HttpCode);
            info.AddValue("Code", this.Code);

            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Initialize the exception with the default values.
        /// </summary>
        private void Initialize()
        {
            this.Handled = false;
            this.Action = HandleAction.SendToLog;
        }
    }
}