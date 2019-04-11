namespace BasketService.Application.Messaging.Producers
{
    using System.Threading.Tasks;

    public interface IBasketCreatedProducer
    {
        Task ProduceBasketCreatedEventAsync(string basketId);
    }
}