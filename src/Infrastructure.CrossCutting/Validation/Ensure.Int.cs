namespace BasketService.Infrastructure.CrossCutting.Validation
{
    using System;

    public static partial class Ensure
    {
        public static void IsNonZero(int value, string message = null)
        {
            Is(value != 0, message);
        }

        public static void IsNonZero<TException>(int value, string message = null)
            where TException : Exception
        {
            Is<TException>(value != 0, message);
        }
    }
}