namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    using System;

    [Serializable]
    public class ErrorDetail
    {
        private const string DefaultErrorMessage = "An error has ocurred. Please check application logs for more information.";

        public ErrorDetail(Exception ex, int errorCode, bool returnException)
        {
            this.ErrorCode = errorCode;
            this.Message = string.IsNullOrWhiteSpace(ex.Message)? DefaultErrorMessage : ex.Message;

            if (returnException)
            {
                this.Errors = Error.FromException(ex);
            }
        }

        public int ErrorCode { get; private set; }

        public string Message { get; private set; }

        public Error[] Errors { get; private set; }
    }
}
