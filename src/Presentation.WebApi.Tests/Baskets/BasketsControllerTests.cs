namespace BasketService.Presentation.WebApi.Tests.Baskets
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.UseCases.RequestModel;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Presentation.WebApi", "BasketsControllerTests")]
    [Collection("PresentationTestsCollection")]
    public class BasketsControllerTests
    {
        private Fixture fixture;

        public BasketsControllerTests()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public async Task GetBasketsById_NoParams_ServiceCalledOnce()
        {
            //Arrange
            var basketId = Guid.NewGuid().ToString();
            var mock = BasketsControllerMock.Create();
            var target = mock.Target;

            var expected = this.fixture.Create<Basket>();

            mock.GetBasketsByIdMock
                .Setup(iS => iS.Execute(It.IsAny<GetBasketsByIdRequest>()))
                .ReturnsAsync(expected);

            //Act
            var actual = (OkObjectResult)await target.GetBaskets(basketId);

            //Assert
            mock.GetBasketsByIdMock.Verify(iS => iS.Execute(It.IsAny<GetBasketsByIdRequest>()), Times.Once);
        }
    }
}