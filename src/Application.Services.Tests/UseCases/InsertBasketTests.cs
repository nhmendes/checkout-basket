namespace BasketService.Application.Services.Tests.UseCases
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Domain.Model.Baskets;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Application.Services", "InsertBasket")]
    [Collection("ApplicationServicesTestsCollection")]
    public class InsertBasketTests
    {
        [Fact]
        public async Task InsertBasket_ValidRequest_RepositoryCalledOnce()
        {
            //Arrange
            var customerEmail = "customer@email.com";
            var mock = InsertBasketMock.Create();
            var target = mock.Target;
            var request = InsertBasketRequest.Create(customerEmail);

            //Act
            await target.Execute(request);

            //Assert
            mock.BasketsRepositoryMock.Verify(rep => rep.Insert(It.IsAny<Basket>()), Times.Once());
        }

        [Fact]
        public async Task InsertBasket_ValidRequest_ProducerCalledOnce()
        {
            //Arrange
            var customerEmail = "customer@email.com";
            var mock = InsertBasketMock.Create();
            var target = mock.Target;
            var request = InsertBasketRequest.Create(customerEmail);

            //Act
            await target.Execute(request);

            //Assert
            mock.BasketCreatedProducerMock.Verify(x => x.ProduceBasketCreatedEventAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task InsertBasket_NullRequest_ThrowsException()
        {
            //Arrange
            var mock = InsertBasketMock.Create();
            var target = mock.Target;
            InsertBasketRequest request = null;

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(execute);
        }

        [Fact]
        public async Task InsertBasket_NullCustomerEmail_ThrowsException()
        {
            //Arrange
            string customerEmail = null;
            var mock = InsertBasketMock.Create();
            var target = mock.Target;
            var request = InsertBasketRequest.Create(customerEmail);

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(execute);
        }
    }
}