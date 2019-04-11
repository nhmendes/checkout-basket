namespace BasketService.Client.Tests.Baskets
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Client.HttpConnection;
    using BasketService.Client.Baskets;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Client", "BasketsClient")]
    [Collection("ClientTestsCollection")]
    public class BasketsClientTests
    {
        private const string BaseUri = "http://localhost";
        private const string Controller = "Baskets";
        private readonly Fixture fixture;
        private readonly Mock<IHttpConnection> httpConnectionMock;

        public BasketsClientTests()
        {
            this.fixture = new Fixture();
            this.httpConnectionMock = new Mock<IHttpConnection>();
        }

        [Fact]
        public async Task GetBasketById_ReturnId_CorrectResults()
        {
            //Arrange
            var basketId = Guid.NewGuid().ToString();
            var expectedUri = new Uri($"{BaseUri}/{Controller}/{basketId}");
            var expected = this.fixture.Create<Basket>();

            this.httpConnectionMock
                .Setup(x => x.BaseAddress)
                .Returns(BaseUri);
            this.httpConnectionMock
                .Setup(x => x.GetAsync<Basket>(expectedUri))
                .ReturnsAsync(expected);

            var target = new BasketsClient(this.httpConnectionMock.Object);

            //Act
            var actual = await target.GetBasketAsync(basketId);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}