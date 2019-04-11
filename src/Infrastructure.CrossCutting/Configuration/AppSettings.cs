namespace BasketService.Infrastructure.CrossCutting.Configuration
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class AppSettings : IAppSettings
    {
        public string LoggingLevel { get; set; }
    }
}