namespace BasketService.Presentation.WebApi.IoCContainer
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class GatewayRegisterTypesExtension
    {
        internal static IServiceCollection RegisterGatewayRegisterTypes(this IServiceCollection services)
        {
            //services.AddTransient<, >();
            return services;
        }        
    }
}