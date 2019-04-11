namespace BasketService.Presentation.WebApi.Configurations
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Infrastructure.CrossCutting.Configuration;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class LoggingConfigurationExtension
    {
        private const string BasketServiceSection = "BasketService";

        internal static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var appSettings = configuration.GetSection(BasketServiceSection).Get<AppSettings>();

            if (!Enum.TryParse(appSettings.LoggingLevel, out LogLevel logLevel))
            {
                logLevel = LogLevel.Info;
            }

            services.AddSingleton<ILog, Logger>();

            return services;
        }
    }
}