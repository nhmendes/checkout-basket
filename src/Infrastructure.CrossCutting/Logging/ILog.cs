namespace BasketService.Infrastructure.CrossCutting.Logging
{
    using System;

    public interface ILog
    {
        void Error(string message);
        void Error(string message, System.Exception ex);
        void Error(string message, Func<object> dataFunc);
        void Info(string message);
        void Info(string message, Func<object> dataFunc);
        void Warning(string message);
        void Warning(string message, Func<object> dataFunc);
        void Verbose(string message);
        void Verbose(string message, Func<object> dataFunc);
    }
}