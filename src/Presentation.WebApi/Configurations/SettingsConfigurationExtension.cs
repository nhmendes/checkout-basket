namespace BasketService.Presentation.WebApi.Configurations
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Infrastructure.CrossCutting.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class SettingsConfigurationExtension
    {
        private const string KafkaSection = "Kafka";
        private const string BasketServiceSection = "BasketService";

        internal static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services
                .Configure<AppSettings>(configuration.GetSection(BasketServiceSection))
                ;

            services.AddSingleton<IAppSettings>(factory =>
            {
                return configuration.GetSection(BasketServiceSection).Get<AppSettings>();
            });

            return services;
        }
    }
}