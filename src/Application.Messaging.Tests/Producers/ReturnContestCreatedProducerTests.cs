namespace BasketService.Application.Messaging.Tests.Producers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using BasketService.Application.KafkaContracts.Events.Contracts;
    using BasketService.Application.KafkaContracts.Events.Contracts.V1;
    using BasketService.Application.Messaging.Exceptions;
    using BasketService.Domain.Model.ReturnContests;
    using BasketService.Domain.Model.Returns;
    using Moq;
    using Xunit;
    using static BasketService.Application.KafkaContracts.Events.Contracts.Error;

    [ExcludeFromCodeCoverage]
    [Trait("Application.Services", "ReturnContestCreatedProducer")]
    [Collection("ApplicationServicesTestsCollection")]
    public class ReturnContestCreatedProducerTests
    {
        protected Fixture fixture;

        public ReturnContestCreatedProducerTests()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public async Task ProduceReturnContestCreatedEventAsync_ValidParam_CallTimesOnce()
        {
            //Arrange
            var mock = ReturnContestCreatedProducerMock.Create();
            var target = mock.Target;

            //Use Mock<Error> when Error class have HasError property as virtual
            var error = new Error() { ErrorCode = ErrorCodes.NoError };

            var tenantId = fixture.Create<int>();
            var contest = fixture.Create<ReturnContest>();
            var returnInfo = fixture.Create<Return>();
            var report1 = fixture.Create<ProductionReport<ReturnContestCreatedV1>>();
            var report = fixture.Build<ProductionReport<ReturnContestCreatedV1>>()
                               .With(r => r.Error, error)
                               .Create();

            mock.ProducerMock
                .Setup(i => i.ProduceAsync(It.IsAny<ReturnContestCreatedV1>()))
                .Returns(Task.FromResult(report));

            //Act
            await target.ProduceReturnContestCreatedEventAsync(tenantId, contest, returnInfo);

            //Assert
            mock.ProducerMock.Verify(i => i.ProduceAsync(It.IsAny<ReturnContestCreatedV1>()), Times.Once);
        }

        [Fact]
        public async Task ProduceReturnContestCreatedEventAsync_ValidParam_ThrowEventProducerException()
        {
            //Arrange
            var mock = ReturnContestCreatedProducerMock.Create();
            var target = mock.Target;

            //Use Mock<Error> when Error class have HasError property as virtual
            var error = new Error() { ErrorCode = ErrorCodes.Unknown };

            var contest = fixture.Create<ReturnContest>();
            var returnInfo = fixture.Create<Return>();
            var tenantId = fixture.Create<int>();
            var report1 = fixture.Create<ProductionReport<ReturnContestCreatedV1>>();
            var report = fixture.Build<ProductionReport<ReturnContestCreatedV1>>()
                               .With(r => r.Error, error)
                               .Create();

            mock.ProducerMock
                .Setup(i => i.ProduceAsync(It.IsAny<ReturnContestCreatedV1>()))
                .Returns(Task.FromResult(report));

            //Act
            Func<Task> action = async () => await target.ProduceReturnContestCreatedEventAsync(tenantId, contest, returnInfo);

            //Assert
            await Assert.ThrowsAsync<EventProducerException>(action);
        }
    }
}