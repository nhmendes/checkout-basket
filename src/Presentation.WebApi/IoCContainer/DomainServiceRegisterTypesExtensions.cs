namespace BasketService.Presentation.WebApi.IoCContainer
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class DomainServiceRegisterTypesExtensions
    {
        internal static IServiceCollection RegisterDomainServicesTypes(this IServiceCollection services)
        {
            //services.AddTransient<, >();

            return services;
        }
    }
}