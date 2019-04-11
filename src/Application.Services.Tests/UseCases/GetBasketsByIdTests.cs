namespace BasketService.Application.Services.Tests.UseCases
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using BasketService.Application.Services.UseCases.RequestModel;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Application.Services", "GetBasketsById")]
    [Collection("ApplicationServicesTestsCollection")]
    public class GetBasketsByIdTests
    {
        [Fact]
        public async Task GetBasketsById_WithId_ReturnsBasket()
        {
            //Arrange
            var basketId = Guid.NewGuid().ToString();
            var mock = GetBasketsByIdMock.Create();
            var target = mock.Target;
            var request = GetBasketsByIdRequest.Create(basketId);

            //Act
            var actual = await target.Execute(request);

            //Assert
            mock.BasketsRepositoryMock.Verify(rep => rep.Get(basketId), Times.Once());
        }

        [Fact]
        public async Task GetBasketsById_NullRequest_ThrowsException()
        {
            //Arrange
            var mock = GetBasketsByIdMock.Create();
            var target = mock.Target;
            GetBasketsByIdRequest request = null;

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(execute);
        }

        [Fact]
        public async Task GetBasketsById_NullBasketId_ThrowsException()
        {
            //Arrange
            string basketId = null;
            var mock = GetBasketsByIdMock.Create();
            var target = mock.Target;
            var request = GetBasketsByIdRequest.Create(basketId);

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(execute);
        }
    }
}