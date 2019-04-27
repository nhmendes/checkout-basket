namespace BasketService.Application.Messaging.Producers
{
    using System.Threading.Tasks;
    using BasketService.Infrastructure.CrossCutting.Logging;

    public class BasketCreatedProducer : IBasketCreatedProducer
    {
        private readonly ILog logger;

        public BasketCreatedProducer(
            ILog logger)
        {
            this.logger = logger;
        }

        public async Task ProduceBasketCreatedEventAsync(string basketId)
        {
            await Task.Run(() => this.logger.Info($"basket created:{basketId}"));
        }
    }
}