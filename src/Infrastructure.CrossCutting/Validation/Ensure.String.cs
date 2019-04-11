namespace BasketService.Infrastructure.CrossCutting.Validation
{
    using System;

    public static partial class Ensure
    {
        public static void IsNotNullOrEmpty(string value, string message = null)
        {
            Is(!string.IsNullOrEmpty(value), message);
        }

        public static void IsNotNullOrEmpty<TException>(string value, string message = null)
            where TException : Exception
        {
            Is<TException>(!string.IsNullOrEmpty(value), message);
        }
    }
}