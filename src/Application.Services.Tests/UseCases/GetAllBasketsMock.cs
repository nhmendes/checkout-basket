namespace BasketService.Application.Services.Tests.UseCases
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Implementations;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using Moq;

    [ExcludeFromCodeCoverage]
    internal class GetAllBasketsMock
    {
        public Mock<IBasketsRepository> BasketsRepositoryMock { get; private set; }
        public Mock<IBasketsTypeAdapter> BasketsTypeAdapterMock { get; private set; }

        public GetAllBaskets Target { get; private set; }

        private GetAllBasketsMock()
        {
            this.BasketsRepositoryMock = new Mock<IBasketsRepository>();
            this.BasketsTypeAdapterMock = new Mock<IBasketsTypeAdapter>();
            this.Target = new GetAllBaskets(
                BasketsRepositoryMock.Object,
                BasketsTypeAdapterMock.Object
                );
        }

        public static GetAllBasketsMock Create()
        {
            return new GetAllBasketsMock();
        }
    }
}