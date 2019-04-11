namespace BasketService.Application.Services.UseCases.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Infrastructure.CrossCutting.Validation;

    /// <summary>
    /// Represents the GetAllBaskets use case.
    /// This use case is used to get all baskets for a given user.
    /// </summary>
    public class GetAllBaskets : IGetAllBaskets
    {
        private readonly IBasketsRepository basketsRepository;
        private readonly IBasketsTypeAdapter basketsTypeAdapter;

        public GetAllBaskets(
            IBasketsRepository basketsRepository,
            IBasketsTypeAdapter basketsTypeAdapter)
        {
            this.basketsRepository = basketsRepository;
            this.basketsTypeAdapter = basketsTypeAdapter;
        }

        /// <summary>
        /// Executes the get all baskets use case.
        /// </summary>
        /// <param name="request">The request with the basket filter to retriev.</param>
        /// <returns>Returns a collection of baskets.</returns>
        public async Task<IEnumerable<Basket>> Execute(GetAllBasketsRequest request)
        {
            Ensure.IsNotNull(request);

            var basket = await this.basketsRepository.GetAll();
            return this.basketsTypeAdapter.ToDto(basket);
        }
    }
}