namespace BasketService.Application.Services.UseCases.Interfaces
{
    using System.Collections.Generic;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.UseCases.RequestModel;

    public interface IGetAllBaskets : IQueryAction<GetAllBasketsRequest, IEnumerable<Basket>> { }
}