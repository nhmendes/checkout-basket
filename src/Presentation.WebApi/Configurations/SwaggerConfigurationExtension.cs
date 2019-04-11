namespace BasketService.Presentation.WebApi.Configurations
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;

    [ExcludeFromCodeCoverage]
    internal static class SwaggerConfigurationExtension
    {
        internal static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfigurationRoot configuration)
        {
            if (!bool.Parse(configuration["swagger:enabled"]))
            {
                return app;
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/api-docs/swagger.json", "Baskets Service");
            });

            return app;
        }

        internal static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfigurationRoot configuration)
        {
            if (!bool.Parse(configuration["swagger:enabled"]))
            {
                return services;
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("api-docs", new Info
                {
                    Version = "v1",
                    Title = "Baskets API"
                });

                c.CustomSchemaIds(x => x.FullName);

                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        Type = "apiKey",
                    });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });
            });

            return services;
        }
    }
}