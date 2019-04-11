namespace BasketService.Infrastructure.CrossCutting.Validation
{
    using System;

    /// <summary>
    /// Helper class which allow prettier code for guard clauses
    /// </summary>
    /// <remarks>Adapted from Portal.</remarks>
    public static partial class Ensure
    {
        /// <summary>
        /// Will throw exception of type <typeparamref name="ArgumentException"/> with the specified message
        /// if the assertion is false
        /// </summary>
        /// <param name="assertion">If set to <c>false</c> [assertion].</param>
        /// <param name="message">The message.</param>
        public static void Is(bool assertion, string message)
        {
            Is<ArgumentException>(assertion, message, innerException: null);
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/> with the specified message
        /// if the assertion is false
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="assertion">If set to <c>false</c> [assertion].</param>
        /// <param name="message">The message.</param>
        public static void Is<TException>(bool assertion, string message)
            where TException : Exception
        {
            Is<TException>(assertion, message, innerException: null);
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/> with the specified message
        /// if the assertion is false
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="assertion">If set to <c>false</c> [assertion].</param>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">An object array that contains the elements for <paramref name="message"/>.</param>
        public static void Is<TException>(bool assertion, string messageFormat, params object[] args)
           where TException : Exception
        {
            Is<TException>(assertion, string.Format(messageFormat, args), innerException: null);
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/> with the specified message
        /// if the assertion is false
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="assertion">If set to <c>false</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.ApplicationException">
        /// Thrown when the type <typeparamref name="TException"/> is incorrectly defined.
        /// </exception>
        public static void Is<TException>(bool assertion, string message, Exception innerException)
            where TException : Exception
        {
            if (assertion)
            {
                return;
            }

            var ctor = typeof(TException).GetConstructor(new[] { typeof(string), typeof(Exception) });

            if (ctor != null)
            {
                throw (TException)ctor.Invoke(new object[] { message, innerException });
            }
            else
            {
                var safeInnerException = new Exception(message, innerException);
                throw new ApplicationException(
                    string.Format("The exception type {0} doesn't support the required constructors", typeof(TException)),
                    safeInnerException);
            }
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/> with the specified message
        /// if the assertion is false
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="assertion">If set to <c>false</c> [assertion].</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">An object array that contains the elements for <paramref name="message"/>.</param>
        public static void Is<TException>(bool assertion, Exception innerException, string messageFormat, params object[] args)
            where TException : Exception
        {
            Is<TException>(assertion, string.Format(messageFormat, args), innerException);
        }

        /// <summary>
        /// Throws exception of type <typeparamref name="TException"/>
        /// if <paramref name="obj"/> is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="obj">The object.</param>
        public static void IsNotNull(object obj, string message = null)
        {
            Is<ArgumentNullException>(obj != null, message);
        }

        /// <summary>
        /// Throws exception of type <typeparamref name="TException"/>
        /// if <paramref name="obj"/> is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="obj">The object.</param>
        public static void IsNotNull<TException>(object obj, string message = null)
            where TException : Exception
        {
            Is<TException>(obj != null, message);
        }
    }
}