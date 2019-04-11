namespace BasketService.Application.Services.UseCases.Interfaces
{
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.UseCases.RequestModel;

    public interface IInsertBasketItem : ICommandAction<InsertBasketItemRequest, Item> { }
}