namespace BasketService.Application.Messaging.Exceptions
{
    using System;

    public class EventProducerException : Exception
    {
        public EventProducerException(string message) : base(message)
        {
        }
    }
}