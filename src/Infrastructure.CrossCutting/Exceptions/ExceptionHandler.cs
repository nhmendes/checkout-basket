namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    using System;
    using System.Text;
    using BasketService.Infrastructure.CrossCutting.Logging;

    public static class ExceptionHandler
    {
        public static bool IsApiBaseException(Exception exception)
        {
            return typeof(ApiExceptionBase).IsAssignableFrom(exception.GetType());
        }

        public static void HandleException(Exception exception, ILog logger)
        {
            LogException(exception, logger);

            if (IsApiBaseException(exception))
            {
                HandleException(exception as ApiExceptionBase);
            }
        }

        private static void HandleException(ApiExceptionBase exception)
        {
            exception.Handled = true;
        }

        private static void LogException(Exception exception, ILog logger)
        {
            if (IsApiBaseException(exception))
            {
                var apiException = exception as ApiExceptionBase;

                if (apiException.Action == HandleAction.None || apiException.Handled)
                {
                    return;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ExceptionHandler - Exception:");
            sb.AppendLine(exception.ToString());

            logger.Error(sb.ToString());
        }
    }
}
