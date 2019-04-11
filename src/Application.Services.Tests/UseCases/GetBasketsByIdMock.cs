namespace BasketService.Application.Services.Tests.UseCases
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Implementations;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using Moq;

    [ExcludeFromCodeCoverage]
    internal class GetBasketsByIdMock
    {
        public Mock<IBasketsRepository> BasketsRepositoryMock { get; private set; }
        public Mock<IBasketsTypeAdapter> BasketsTypeAdapterMock { get; private set; }

        public GetBasketsById Target { get; private set; }

        private GetBasketsByIdMock()
        {
            this.BasketsRepositoryMock = new Mock<IBasketsRepository>();
            this.BasketsTypeAdapterMock = new Mock<IBasketsTypeAdapter>();
            this.Target = new GetBasketsById(
                BasketsRepositoryMock.Object,
                BasketsTypeAdapterMock.Object
                );
        }

        public static GetBasketsByIdMock Create()
        {
            return new GetBasketsByIdMock();
        }
    }
}