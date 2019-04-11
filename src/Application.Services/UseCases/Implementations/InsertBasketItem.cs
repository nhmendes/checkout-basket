namespace BasketService.Application.Services.UseCases.Implementations
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Messaging.Producers;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using BasketService.Infrastructure.CrossCutting.Validation;

    /// <summary>
    /// Represents the InsertBasketItem use case.
    /// This use case is used to add items to a given basket.
    /// </summary>
    public class InsertBasketItem : IInsertBasketItem
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketsTypeAdapter basketsTypeAdapter;
        private readonly IBasketUpdatedProducer basketUpdatedProducer;
        private readonly ILog logger;

        public InsertBasketItem(
            IBasketsRepository basketsRepository,
            IBasketsTypeAdapter basketsTypeAdapter,
            IBasketUpdatedProducer basketUpdatedProducer,
            ILog logger)
        {
            this.basketsRepository = basketsRepository;
            this.basketsTypeAdapter = basketsTypeAdapter;
            this.basketUpdatedProducer = basketUpdatedProducer;
            this.logger = logger;
        }

        /// <summary>
        /// Executes the InsertBasketItem use case.
        /// This execution triggers a domain event BasketItemCreated in order to be consumed by the interested parties.
        /// </summary>
        /// <param name="request">The request with the basket item data.</param>
        public async Task<Item> Execute(InsertBasketItemRequest request)
        {
            Item result;
            try
            {
                Ensure.IsNotNull(request);
                Ensure.IsNotNullOrEmpty(request.BasketId);
                Ensure.IsNotNullOrEmpty(request.ItemVariant);

                var domainItem = Domain.Model.Baskets.Item.Create(request.ItemVariant, request.Quantity);
                await this.basketsRepository.InsertItem(request.BasketId, domainItem);
                await this.PublishBasketItemCreated(request.BasketId);
                result = this.basketsTypeAdapter.ToDto(domainItem);
            }
            catch (Exception ex)
            {
                this.logger.Error(
                    $"{nameof(InsertBasket)}.{nameof(Execute)}",
                    ex);
                throw;
            }
            return result;
        }

        private async Task PublishBasketItemCreated(string basketId)
        {
            await this.basketUpdatedProducer.ProduceBasketUpdatedEventAsync(basketId);
        }
    }
}