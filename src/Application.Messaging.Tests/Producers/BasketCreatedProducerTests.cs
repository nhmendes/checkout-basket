//namespace BasketService.Application.Messaging.Tests.Producers
//{
//    using System.Diagnostics.CodeAnalysis;
//    using System.Threading.Tasks;
//    using AutoFixture;
//    using BasketService.Application.KafkaContracts.Events.Contracts;
//    using BasketService.Application.KafkaContracts.Events.Contracts.V1;
//    using BasketService.Application.Messaging.Exceptions;
//    using BasketService.Domain.Model.Baskets;
//    using Moq;
//    using Xunit;
//    using static BasketService.Application.KafkaContracts.Events.Contracts.Error;

//    [ExcludeFromCodeCoverage]
//    [Trait("Application.Services", "BasketCreatedProducer")]
//    [Collection("ApplicationServicesTestsCollection")]
//    public class BasketCreatedProducerTests
//    {
//        protected Fixture fixture;

//        public BasketCreatedProducerTests()
//        {
//            this.fixture = new Fixture();
//        }

//        [Fact]
//        public async Task ProduceBasketCreatedEventAsync_ValidParam_CallTimesOnce()
//        {
//            //Arrange
//            var mock = ReturnContestCreatedProducerMock.Create();
//            var target = mock.Target;

//            //Use Mock<Error> when Error class have HasError property as virtual
//            var error = new Error() { ErrorCode = ErrorCodes.NoError };

//            var basket = fixture.Create<Basket>();
//            var report1 = fixture.Create<ProductionReport<BasketCreatedV1>>();
//            var report = fixture.Build<ProductionReport<BasketCreatedV1>>().With(r => r.Error, error).Create();

//            mock.ProducerMock
//                .Setup(i => i.ProduceAsync(It.IsAny<BasketCreatedV1>()))
//                .Returns(Task.FromResult(report));

//            //Act
//            await target.ProduceBasketCreatedEventAsync(basket.Id);

//            //Assert
//            mock.ProducerMock.Verify(i => i.ProduceAsync(It.IsAny<BasketCreatedV1>()), Times.Once);
//        }

//        [Fact]
//        public async Task ProduceBasketCreatedEventAsync_ValidParam_ThrowEventProducerException()
//        {
//            //Arrange
//            var mock = ReturnContestCreatedProducerMock.Create();
//            var target = mock.Target;

//            //Use Mock<Error> when Error class have HasError property as virtual
//            var error = new Error() { ErrorCode = ErrorCodes.Unknown };

//            var basket = fixture.Create<Basket>();
//            var report1 = fixture.Create<ProductionReport<BasketCreatedV1>>();
//            var report = fixture.Build<ProductionReport<BasketCreatedV1>>().With(r => r.Error, error).Create();

//            mock.ProducerMock
//                .Setup(i => i.ProduceAsync(It.IsAny<BasketCreatedV1>()))
//                .Returns(Task.FromResult(report));

//            //Act
//            async Task action() => await target.ProduceBasketCreatedEventAsync(basket.Id);

//            //Assert
//            await Assert.ThrowsAsync<EventProducerException>(action);
//        }
//    }
//}