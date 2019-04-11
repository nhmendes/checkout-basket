namespace BasketService.Application.Services.UseCases.Implementations
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Application.Messaging.Producers;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Infrastructure.CrossCutting.Logging;

    /// <summary>
    /// Represents the DeleteBasketItem use case.
    /// This use case is used to delete an item from a basket.
    /// </summary>
    public class DeleteBasketItem : IDeleteBasketItem
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketUpdatedProducer basketUpdatedProducer;
        private readonly ILog logger;

        /// <summary>
        /// Creates an instance of DeleteBasketItem
        /// </summary>
        public DeleteBasketItem(
            IBasketsRepository basketsRepository,
            IBasketUpdatedProducer basketUpdatedProducer,
            ILog logger)

        {
            this.basketsRepository = basketsRepository;
            this.basketUpdatedProducer = basketUpdatedProducer;
            this.logger = logger;
        }

        /// <summary>
        /// Executes the delete item from basket use case.
        /// This execution triggers a domain event BasketUpdated in order to be consumed by the interested parties.
        /// </summary>
        /// <param name="request">The request with the basket item data to delete.</param>
        public async Task Execute(DeleteBasketItemRequest request)
        {
            try
            {
                await this.basketsRepository.DeleteItem(request.BasketId, request.ItemId);
                await this.PublishBasketDeleted(request.BasketId);
            }
            catch (Exception ex)
            {
                this.logger.Error(
                    $"{nameof(InsertBasket)}.{nameof(Execute)}",
                    ex);
                throw;
            }
        }

        private async Task PublishBasketDeleted(string basketId)
        {
            await this.basketUpdatedProducer.ProduceBasketUpdatedEventAsync(basketId);
        }
    }
}