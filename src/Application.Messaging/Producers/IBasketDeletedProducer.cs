namespace BasketService.Application.Messaging.Producers
{
    using System.Threading.Tasks;

    public interface IBasketDeletedProducer
    {
        Task ProduceBasketDeletedEventAsync(string basketId);
    }
}