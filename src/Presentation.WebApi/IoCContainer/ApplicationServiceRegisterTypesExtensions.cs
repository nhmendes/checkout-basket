namespace BasketService.Presentation.WebApi.IoCContainer
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.Services.TypeAdapters;
    using BasketService.Application.Services.UseCases.Implementations;
    using BasketService.Application.Services.UseCases.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class ApplicationServiceRegisterTypesExtensions
    {
        internal static IServiceCollection RegisterApplicationServicesTypes(this IServiceCollection services)
        {
            services.AddTransient<IGetAllBaskets, GetAllBaskets>();
            services.AddTransient<IGetBasketsById, GetBasketsById>();
            services.AddTransient<IInsertBasket, InsertBasket>();
            services.AddTransient<IDeleteBasket, DeleteBasket>();

            services.AddTransient<IInsertBasketItem, InsertBasketItem>();
            services.AddTransient<IGetBasketItemById, GetBasketItemById>();
            services.AddTransient<IDeleteBasketItem, DeleteBasketItem>();
            services.AddTransient<IUpdateBasketItem, UpdateBasketItem>();

            services.AddTransient<IBasketsTypeAdapter, BasketsTypeAdapter>();

            return services;
        }
    }
}