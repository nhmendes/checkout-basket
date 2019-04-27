namespace BasketService.Application.Messaging.Producers
{
    using System.Threading.Tasks;
    using BasketService.Infrastructure.CrossCutting.Logging;

    public class BasketUpdatedProducer : IBasketUpdatedProducer
    {
        private readonly ILog logger;

        public BasketUpdatedProducer(
            ILog logger)
        {
            this.logger = logger;
        }

        public async Task ProduceBasketUpdatedEventAsync(string basketId)
        {
            await Task.Run(() => this.logger.Info($"basket updated:{basketId}"));
        }
    }
}