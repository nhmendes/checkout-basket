namespace BasketService.Application.Services.UseCases.Interfaces
{
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.UseCases.RequestModel;
    //using BasketService.Domain.Model.Baskets;

    public interface IInsertBasket : ICommandAction<InsertBasketRequest, Basket> { }
}