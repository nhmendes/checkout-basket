namespace BasketService.Application.Services.UseCases.Implementations
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Domain.Model.Baskets;
    using BasketService.Infrastructure.CrossCutting.Logging;

    public class UpdateBasketItem : IUpdateBasketItem
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketsTypeAdapter basketsTypeAdapter;
        private readonly ILog logger;

        public UpdateBasketItem(
            IBasketsRepository basketsRepository,
            IBasketsTypeAdapter basketsTypeAdapter,
            ILog logger)
        {
            this.basketsRepository = basketsRepository;
            this.basketsTypeAdapter = basketsTypeAdapter;
            this.logger = logger;
        }

        public async Task Execute(UpdateBasketItemRequest request)
        {
            try
            {
                var domainItem = this.basketsTypeAdapter.ToDomain(request.Item);
                await this.basketsRepository.UpdateItem(request.BasketId, domainItem);
                await this.PublishBasketItemUpdated(domainItem);
            }
            catch (Exception ex)
            {
                this.logger.Error(
                    $"{nameof(InsertBasket)}.{nameof(Execute)}",
                    ex);
                throw;
            }
        }

        private async Task PublishBasketItemUpdated(Item domainItem)
        {
            await Task.Run(() =>
                this.logger.Info("BasketItemUpdated", () =>
                {
                    return domainItem;
                }));
        }
    }
}