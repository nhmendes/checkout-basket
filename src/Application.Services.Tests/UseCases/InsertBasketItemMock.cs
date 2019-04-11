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
    internal class InsertBasketItemMock
    {
        public Mock<IBasketsRepository> BasketsRepositoryMock { get; private set; }
        public Mock<IBasketsTypeAdapter> BasketsTypeAdapterMock { get; private set; }
        public Mock<IBasketUpdatedProducer> BasketUpdatedProducerMock { get; private set; }
        public Mock<ILog> LogMock { get; private set; }

        public InsertBasketItem Target { get; private set; }

        private InsertBasketItemMock()
        {
            this.BasketsRepositoryMock = new Mock<IBasketsRepository>();
            this.BasketsTypeAdapterMock = new Mock<IBasketsTypeAdapter>();
            this.BasketUpdatedProducerMock = new Mock<IBasketUpdatedProducer>();
            this.LogMock = new Mock<ILog>();
            this.Target = new InsertBasketItem(
                BasketsRepositoryMock.Object,
                BasketsTypeAdapterMock.Object,
                BasketUpdatedProducerMock.Object,
                LogMock.Object
                );
        }

        public static InsertBasketItemMock Create()
        {
            return new InsertBasketItemMock();
        }
    }
}