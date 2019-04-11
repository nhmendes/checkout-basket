namespace BasketService.Infrastructure.CrossCutting.Logging
{
    using System;
    using System.Diagnostics;

    public class Logger : ILog
    {
        public Logger() { }

        public void Error(string message)
        {
            Debug.WriteLine(message);
        }

        public void Error(string message, Exception ex)
        {
            Debug.WriteLine(message);
        }

        public void Error(string message, Func<object> dataFunc)
        {
            Debug.WriteLine(message);
        }

        public void Info(string message)
        {
            Debug.WriteLine(message);
        }

        public void Info(string message, Func<object> dataFunc)
        {
            Debug.WriteLine(message);
        }

        public void Verbose(string message)
        {
            Debug.WriteLine(message);
        }

        public void Verbose(string message, Func<object> dataFunc)
        {
            Debug.WriteLine(message);
        }

        public void Warning(string message)
        {
            Debug.WriteLine(message);
        }

        public void Warning(string message, Func<object> dataFunc)
        {
            Debug.WriteLine(message);
        }
    }
}