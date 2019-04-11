namespace BasketService.Application.Services.Tests.UseCases
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.Messaging.Producers;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Implementations;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Moq;

    [ExcludeFromCodeCoverage]
    internal class InsertBasketMock
    {
        public Mock<IBasketsRepository> BasketsRepositoryMock { get; private set; }
        public Mock<IBasketsTypeAdapter> BasketsTypeAdapterMock { get; private set; }
        public Mock<IBasketCreatedProducer> BasketCreatedProducerMock { get; private set; }
        public Mock<ILog> LogMock { get; private set; }

        public InsertBasket Target { get; private set; }

        private InsertBasketMock()
        {
            this.BasketsRepositoryMock = new Mock<IBasketsRepository>();
            this.BasketsTypeAdapterMock = new Mock<IBasketsTypeAdapter>();
            this.BasketCreatedProducerMock = new Mock<IBasketCreatedProducer>();
            this.LogMock = new Mock<ILog>();
            this.Target = new InsertBasket(
                BasketsRepositoryMock.Object,
                BasketsTypeAdapterMock.Object,
                BasketCreatedProducerMock.Object,
                LogMock.Object
                );
        }

        public static InsertBasketMock Create()
        {
            return new InsertBasketMock();
        }
    }
}