namespace BasketService.Application.Services.Tests.UseCases
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.Messaging.Producers;
    using BasketService.Application.Services.UseCases.Implementations;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Moq;

    [ExcludeFromCodeCoverage]
    internal class DeleteBasketMock
    {
        public Mock<IBasketsRepository> BasketsRepositoryMock { get; private set; }
        public Mock<IBasketDeletedProducer> BasketDeletedProducerMock { get; private set; }
        public Mock<ILog> LogMock { get; private set; }

        public DeleteBasket Target { get; private set; }

        private DeleteBasketMock()
        {
            this.BasketsRepositoryMock = new Mock<IBasketsRepository>();
            this.BasketDeletedProducerMock = new Mock<IBasketDeletedProducer>();
            this.LogMock = new Mock<ILog>();
            this.Target = new DeleteBasket(
                BasketsRepositoryMock.Object,
                BasketDeletedProducerMock.Object,
                LogMock.Object
                );
        }

        public static DeleteBasketMock Create()
        {
            return new DeleteBasketMock();
        }
    }
}