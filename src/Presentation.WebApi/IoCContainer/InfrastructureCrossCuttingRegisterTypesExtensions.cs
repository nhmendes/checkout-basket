namespace BasketService.Presentation.WebApi.IoCContainer
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class InfrastructureCrossCuttingRegisterTypesExtensions
    {
        internal static IServiceCollection RegisterInfrastructureCrossCuttingTypes(this IServiceCollection services)
        {
            services.AddSingleton<ILog, Logger>();

            return services;
        }
    }
}