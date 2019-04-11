namespace BasketService.Presentation.WebApi.IoCContainer
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.Messaging.Producers;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class MessagingTypesExtensions
    {
        internal static IServiceCollection RegisterMessagingTypes(this IServiceCollection services)
        {
            services.AddSingleton<IBasketCreatedProducer, BasketCreatedProducer>();

            services.AddSingleton<IBasketUpdatedProducer, BasketUpdatedProducer>();

            services.AddSingleton<IBasketDeletedProducer, BasketDeletedProducer>();

            return services;
        }
    }
}