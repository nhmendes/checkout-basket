namespace BasketService.Application.Services.UseCases.Implementations
{
    using System.Threading.Tasks;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Infrastructure.CrossCutting.Validation;

    /// <summary>
    /// Represents the GetBasketsById use case. This use case returns the basket of a given id.
    /// </summary>
    public class GetBasketsById : IGetBasketsById
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketsTypeAdapter basketsTypeAdapter;

        /// <summary>
        /// Creates an instance of GetBasketsById
        /// </summary>
        public GetBasketsById(
            IBasketsRepository basketsRepository,
            IBasketsTypeAdapter basketsTypeAdapter)
        {
            this.basketsRepository = basketsRepository;
            this.basketsTypeAdapter = basketsTypeAdapter;
        }

        /// <summary>
        /// Executes the GetBasketsById use case
        /// </summary>
        /// <param name="request">The request filter to be applied</param>
        /// <returns>Returns the Basket for the argument filter request</returns>
        public async Task<Basket> Execute(GetBasketsByIdRequest request)
        {
            Ensure.IsNotNull(request);
            Ensure.IsNotNullOrEmpty(request.BasketId);

            var basket = await this.basketsRepository.Get(request.BasketId);

            return this.basketsTypeAdapter.ToDto(basket);
        }
    }
}