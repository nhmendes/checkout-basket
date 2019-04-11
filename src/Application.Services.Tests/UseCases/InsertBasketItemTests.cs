namespace BasketService.Application.Services.Tests.UseCases
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Model.Baskets;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Application.Services", "InsertBasketItem")]
    [Collection("ApplicationServicesTestsCollection")]
    public class InsertBasketItemTests
    {
        [Fact]
        public async Task InsertBasketItem_ValidRequest_RepositoryCalledOnce()
        {
            //Arrange
            var fixture = new Fixture();
            var basketId = fixture.Create<string>();
            var itemVariant = fixture.Create<string>();
            var quantity = 2;
            var mock = InsertBasketItemMock.Create();
            var target = mock.Target;
            var request = InsertBasketItemRequest.Create(basketId, itemVariant, quantity);

            //Act
            await target.Execute(request);

            //Assert
            mock.BasketsRepositoryMock.Verify(rep => rep.InsertItem(basketId, It.IsAny<Item>()), Times.Once());
        }
        
        [Fact]
        public async Task InsertBasketItem_ValidRequest_ProducerCalledOnce()
        {
            //Arrange
            var fixture = new Fixture();
            var basketId = fixture.Create<string>();
            var itemVariant = fixture.Create<string>();
            var quantity = fixture.Create<int>();
            var mock = InsertBasketItemMock.Create();
            var target = mock.Target;
            var request = InsertBasketItemRequest.Create(basketId, itemVariant, quantity);

            //Act
            await target.Execute(request);

            //Assert
            mock.BasketUpdatedProducerMock.Verify(x => x.ProduceBasketUpdatedEventAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task InsertBasketItem_NullRequest_ThrowsException()
        {
            //Arrange
            var mock = InsertBasketItemMock.Create();
            var target = mock.Target;
            InsertBasketItemRequest request = null;

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(execute);
        }

        [Fact]
        public async Task InsertBasketItem_NullBasketId_ThrowsException()
        {
            //Arrange
            var fixture = new Fixture();
            string basketId = null;
            var itemVariant = fixture.Create<string>();
            var quantity = fixture.Create<int>();
            var mock = InsertBasketItemMock.Create();
            var target = mock.Target;
            var request = InsertBasketItemRequest.Create(basketId, itemVariant, quantity);

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(execute);
        }

        [Fact]
        public async Task InsertBasketItem_NullVariant_ThrowsException()
        {
            //Arrange
            var fixture = new Fixture();
            var basketId = fixture.Create<string>();
            string itemVariant = null;
            var quantity = fixture.Create<int>();
            var mock = InsertBasketItemMock.Create();
            var target = mock.Target;
            var request = InsertBasketItemRequest.Create(basketId, itemVariant, quantity);

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(execute);
        }
    }
}