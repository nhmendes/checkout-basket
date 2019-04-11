namespace BasketService.Application.Services.UseCases.Interfaces
{
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.UseCases.RequestModel;

    public interface IGetBasketItemById : IQueryAction<GetBasketItemByIdRequest, Item> { }
}