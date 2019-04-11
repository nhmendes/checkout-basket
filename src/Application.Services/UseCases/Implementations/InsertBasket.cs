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
    /// Represents the InsertBasket use case.
    /// This use case is used to create a new basket for a customer.
    /// </summary>
    public class InsertBasket : IInsertBasket
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketsTypeAdapter basketsTypeAdapter;
        private readonly IBasketCreatedProducer basketCreatedProducer;
        private readonly ILog logger;

        /// <summary>
        /// Creates an instance of InsertBasket
        /// </summary>
        public InsertBasket(
            IBasketsRepository basketsRepository,
            IBasketsTypeAdapter basketsTypeAdapter,
            IBasketCreatedProducer basketCreatedProducer,
            ILog logger)
        {
            this.basketsRepository = basketsRepository;
            this.basketsTypeAdapter = basketsTypeAdapter;
            this.basketCreatedProducer = basketCreatedProducer;
            this.logger = logger;
        }

        /// <summary>
        /// Executes the create basket use case.
        /// This execution triggers a domain event BasketCreated in order to be consumed by the interested parties.
        /// </summary>
        /// <param name="request">The request with the new basket data.</param>
        public async Task<Basket> Execute(InsertBasketRequest request)
        {
            Basket result;
            try
            {
                Ensure.IsNotNull(request);
                Ensure.IsNotNullOrEmpty(request.CustomerEmail);

                var domainBasket = Domain.Model.Baskets.Basket.Create(request.CustomerEmail);
                await this.basketsRepository.Insert(domainBasket);
                await this.ProduceBasketCreated(domainBasket);
                result = this.basketsTypeAdapter.ToDto(domainBasket);
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

        /// <summary>
        /// Produces a BasketCreated event
        /// </summary>
        /// <param name="basket">The basket created</param>
        /// <returns></returns>
        private async Task ProduceBasketCreated(Domain.Model.Baskets.Basket basket)
        {
            await this.basketCreatedProducer.ProduceBasketCreatedEventAsync(basket.Id);
        }
    }
}