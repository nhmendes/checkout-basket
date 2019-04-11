namespace BasketService.Infrastructure.CrossCutting.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class Ensure
    {
        public static void IsEmpty<T>(IEnumerable<T> collection, string message = null)
        {
            IsEmpty<T, ArgumentException>(collection, message);
        }

        public static void IsEmpty<T, TException>(IEnumerable<T> collection, string message = null)
            where TException : Exception
        {
            Is<TException>(!collection.Any(), message);
        }

        public static void IsNotEmpty<T>(IEnumerable<T> collection, string message = null)
        {
            IsNotEmpty<T, ArgumentException>(collection, message);
        }

        public static void IsNotEmpty<T, TException>(IEnumerable<T> collection, string message = null)
            where TException : Exception
        {
            Is<TException>(collection.Any(), message);
        }
    }
}