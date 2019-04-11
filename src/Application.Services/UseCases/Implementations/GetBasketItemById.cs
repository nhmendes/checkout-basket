namespace BasketService.Application.Services.UseCases.Implementations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Core.RepositoryInterfaces;

    /// <summary>
    /// Represents the GetBasketItemById use case. 
    /// This use case is used to get a single basket item.
    /// </summary>
    public class GetBasketItemById : IGetBasketItemById
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketsTypeAdapter basketsTypeAdapter;

        public GetBasketItemById(
            IBasketsRepository basketsRepository,
            IBasketsTypeAdapter basketsTypeAdapter)
        {
            this.basketsRepository = basketsRepository;
            this.basketsTypeAdapter = basketsTypeAdapter;
        }

        /// <summary>
        /// Executes the GetBasketItemById use case.
        /// </summary>
        /// <param name="request">The request filter to be applied</param>
        /// <returns>Returns the basket item for the request argument filte</returns>
        public async Task<Item> Execute(GetBasketItemByIdRequest request)
        {
            var basket = await this.basketsRepository.Get(request.BasketId);

            if (null == basket)
            {
                return null;
            }

            var item = basket.Items
                .Where(x => x.ItemId.Equals(request.ItemId, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();

            return this.basketsTypeAdapter.ToDto(item);
        }
    }
}