namespace BasketService.Application.Services.UseCases.Interfaces
{
    using BasketService.Application.Services.UseCases.RequestModel;

    public interface IDeleteBasketItem : ICommandAction<DeleteBasketItemRequest> { }
}