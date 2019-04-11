namespace BasketService.Application.Messaging.Producers
{
    using System.Threading.Tasks;
    using BasketService.Infrastructure.CrossCutting.Logging;

    public class BasketDeletedProducer : IBasketDeletedProducer
    {
        private readonly ILog logger;

        public BasketDeletedProducer(
            ILog logger)
        {
            this.logger = logger;
        }

        public async Task ProduceBasketDeletedEventAsync(string basketId)
        {
            this.logger.Info($"basket deleted:{basketId}");
        }
    }
}