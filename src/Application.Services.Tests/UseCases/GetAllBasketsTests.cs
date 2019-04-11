namespace BasketService.Application.Services.Tests.UseCases
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using BasketService.Application.Services.UseCases.RequestModel;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Application.Services", "GetAllBaskets")]
    [Collection("ApplicationServicesTestsCollection")]
    public class GetAllBasketsTests
    {
        [Fact]
        public async Task GetAllBasketsTests_ValidRequest_RepositoryCalledOnce()
        {
            //Arrange
            var basketId = Guid.NewGuid().ToString();
            var mock = GetAllBasketsMock.Create();
            var target = mock.Target;
            var request = GetAllBasketsRequest.Create();

            //Act
            var actual = await target.Execute(request);

            //Assert
            mock.BasketsRepositoryMock.Verify(rep => rep.GetAll(), Times.Once());
        }

        [Fact]
        public async Task GetBasketsById_NullRequest_ThrowsException()
        {
            //Arrange
            var mock = GetAllBasketsMock.Create();
            var target = mock.Target;
            GetAllBasketsRequest request = null;

            //Act
            async Task execute() => await target.Execute(request);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(execute);
        }
    }
}