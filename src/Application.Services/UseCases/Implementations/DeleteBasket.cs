namespace BasketService.Application.Services.UseCases.Implementations
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Application.Messaging.Producers;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using BasketService.Infrastructure.CrossCutting.Validation;

    /// <summary>
    /// Represents the DeleteBasket use case.
    /// This use case is used to delete a basket.
    /// </summary>
    public class DeleteBasket : IDeleteBasket
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketDeletedProducer basketDeletedProducer;
        private readonly ILog logger;

        /// <summary>
        /// Creates an instance of DeleteBasket
        /// </summary>
        /// <param name="basketsRepository">The baskets repository</param>
        /// <param name="basketDeletedProducer">The basket deleted producer</param>
        /// <param name="logger">A logger object</param>
        public DeleteBasket(
            IBasketsRepository basketsRepository,
            IBasketDeletedProducer basketDeletedProducer,
            ILog logger)

        {
            this.basketsRepository = basketsRepository;
            this.basketDeletedProducer = basketDeletedProducer;
            this.logger = logger;
        }

        /// <summary>
        /// Executes the delete basket use case.
        /// This execution triggers a domain event BasketDeleted in order to be consumed by the interested parties.
        /// </summary>
        /// <param name="request">The request with the basket data to delete.</param>
        public async Task Execute(DeleteBasketRequest request)
        {
            Ensure.IsNotNull(request);
            Ensure.IsNotNullOrEmpty(request.BasketId);

            try
            {
                await this.basketsRepository.Delete(request.BasketId);
                await this.ProduceBasketDeleted(request.BasketId);
            }
            catch (Exception ex)
            {
                this.logger.Error(
                    $"{nameof(InsertBasket)}.{nameof(Execute)}",
                    ex);
                throw;
            }
        }

        private async Task ProduceBasketDeleted(string basketId)
        {
            await this.basketDeletedProducer.ProduceBasketDeletedEventAsync(basketId);
        }
    }
}