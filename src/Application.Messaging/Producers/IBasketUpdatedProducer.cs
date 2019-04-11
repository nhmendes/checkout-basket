namespace BasketService.Application.Messaging.Producers
{
    using System.Threading.Tasks;

    public interface IBasketUpdatedProducer
    {
        Task ProduceBasketUpdatedEventAsync(string basketId);
    }
}