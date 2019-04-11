namespace BasketService.Presentation.WebApi
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Infrastructure.CrossCutting.Configuration;
    using BasketService.Presentation.WebApi.Configurations;
    using BasketService.Presentation.WebApi.Extensions;
    using BasketService.Presentation.WebApi.IoCContainer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    [ExcludeFromCodeCoverage]
    public class Startup
    {
        #region Private properties

        private const string ConfigurationsPath = "conf";
        private const string EnvironmentVariableName = "ENV";

        //SecretKey  for Authentication  
        private const string SecretKey = "ASEFRFDDWSDRGYHF";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        #endregion Private properties

        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            this.ConfigureEnvironment(env);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IAppSettings appSettings,
            ILog log)
        {
            var isDevelopment = env.IsDevelopment();

            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(log);

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc()
               .UseSwagger(this.Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions()
                .ConfigureSettings(this.Configuration)
                .ConfigureSwagger(this.Configuration)
                .ConfigureLogging(this.Configuration)
                ;

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    const string Secret = "1eZyE1T87t+EiwQALT+rN1l8xvn4dnqK5aAHCbwtOuR3IBymRkPA46VNOa6zpdvFY6dpjnyEAJxVvOT4Pem+uA==";
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            
            this.RegisterTypes(services);
        }

        #region Private methods

        private void ConfigureEnvironment(IHostingEnvironment env)
        {
            var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableName) ?? env.EnvironmentName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"{ConfigurationsPath}/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{ConfigurationsPath}/appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        private void RegisterTypes(IServiceCollection services)
        {
            services.RegisterAspNetCoreMvcTypes()
                    .RegisterMessagingTypes()
                    .RegisterDomainServicesTypes()
                    .RegisterApplicationServicesTypes()
                    .RegisterDataRepositoryTypes()
                    .RegisterGatewayRegisterTypes()
                    .RegisterInfrastructureCrossCuttingTypes()
                    ;
        }

        #endregion Private methods
    }
}