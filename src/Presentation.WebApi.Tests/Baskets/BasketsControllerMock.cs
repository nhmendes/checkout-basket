namespace BasketService.Presentation.WebApi.Tests.Baskets
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using BasketService.Presentation.WebApi.Controllers;
    using Moq;

    [ExcludeFromCodeCoverage]
    public class BasketsControllerMock
    {
        public Mock<IGetAllBaskets> GetAllBasketsMock { get; }
        public Mock<IGetBasketsById> GetBasketsByIdMock { get; }
        public Mock<IInsertBasket> InsertBasketMock { get; }
        public Mock<IDeleteBasket> DeleteBasketMock { get; }

        public Mock<ILog> Logger { get; set; }
        public BasketsController Target { get; }

        private BasketsControllerMock()
        {
            this.GetAllBasketsMock = new Mock<IGetAllBaskets>();
            this.GetBasketsByIdMock = new Mock<IGetBasketsById>();
            this.InsertBasketMock = new Mock<IInsertBasket>();
            this.DeleteBasketMock = new Mock<IDeleteBasket>();

            this.Target = new BasketsController(
                GetAllBasketsMock.Object,
                GetBasketsByIdMock.Object,
                InsertBasketMock.Object,
                DeleteBasketMock.Object
                );
        }

        public static BasketsControllerMock Create()
        {
            return new BasketsControllerMock();
        }
    }
}