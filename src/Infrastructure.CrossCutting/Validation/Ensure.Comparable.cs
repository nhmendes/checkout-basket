using System;

namespace BasketService.Infrastructure.CrossCutting.Validation
{
    public static partial class Ensure
    {
        public static void IsGreaterThan<T>(T value, T minValue, string message = null)
            where T : IComparable
        {
            Is(value.CompareTo(minValue) > 0, message);
        }

        public static void IsGreaterThan<T, TException>(T value, T minValue, string message = null)
            where T : IComparable
            where TException : Exception
        {
            Is<TException>(value.CompareTo(minValue) > 0, message);
        }

        public static void IsEqualTo<T>(T value, T otherValue, string message = null)
            where T : IComparable
        {
            Is(value.CompareTo(otherValue) == 0, message);
        }

        public static void IsEqualTo<T, TException>(T value, T otherValue, string message = null)
            where T : IComparable
            where TException : Exception
        {
            Is<TException>(value.CompareTo(otherValue) == 0, message);
        }
    }
}