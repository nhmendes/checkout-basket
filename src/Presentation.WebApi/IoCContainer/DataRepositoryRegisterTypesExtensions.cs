namespace BasketService.Presentation.WebApi.IoCContainer
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Data.Repository.Baskets;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class DataRepositoryRegisterTypesExtensions
    {
        internal static IServiceCollection RegisterDataRepositoryTypes(this IServiceCollection services)
        {
            services.AddSingleton<IBasketsDatabase, MockDatabase>();
            services.AddTransient<IBasketsRepository, BasketsRepository>();

            return services;
        }
    }
}