namespace BasketService.Application.Services.Tests.UseCases
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using BasketService.Application.Services.UseCases.RequestModel;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Application.Services", "DeleteBasket")]
    [Collection("ApplicationServicesTestsCollection")]
    public class DeleteBasketTests
    {
        [Fact]
        public async Task GetAllBasketsTests_ValidRequest_RepositoryCalledOnce()
        {
            //Arrange
            var basketId = Guid.NewGuid().ToString();
            var mock = DeleteBasketMock.Create();
            var target = mock.Target;
            var request = DeleteBasketRequest.Create(basketId);

            //Act
            await target.Execute(request);

            //Assert
            mock.BasketsRepositoryMock.Verify(rep => rep.Delete(basketId), Times.Once());
        }

        [Fact]
        public async Task DeleteBasket_NullRequest_ThrowsException()
        {
            //Arrange
            var mock = DeleteBasketMock.Create();
            var target = mock.Target;
            DeleteBasketRequest request = null;

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(execute);
        }

        [Fact]
        public async Task DeleteBasket_NullBasketId_ThrowsException()
        {
            //Arrange
            string basketId = null;
            var mock = DeleteBasketMock.Create();
            var target = mock.Target;
            var request = DeleteBasketRequest.Create(basketId);

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(execute);
        }
    }
}