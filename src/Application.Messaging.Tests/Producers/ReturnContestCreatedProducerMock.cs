namespace BasketService.Application.Messaging.Tests.Producers
{
    using BasketService.Application.KafkaContracts.Events.Contracts.V1;
    using BasketService.Application.Messaging.Producers;
    using Moq;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using BasketService.Application.KafkaContracts.Events.Contracts;

    public class ReturnContestCreatedProducerMock
    {
        public Mock<IProducer<ReturnContestCreatedV1>> ProducerMock { get; }

        public Mock<ILog> LogMock { get; }

        public BasketCreatedProducer Target { get; }

        private ReturnContestCreatedProducerMock()
        {
            this.ProducerMock = new Mock<IProducer<ReturnContestCreatedV1>>();
            this.LogMock = new Mock<ILog>();
            this.Target = new BasketCreatedProducer(ProducerMock.Object, LogMock.Object);
        }

        public static ReturnContestCreatedProducerMock Create()
        {
            return new ReturnContestCreatedProducerMock();
        }
    }
}
