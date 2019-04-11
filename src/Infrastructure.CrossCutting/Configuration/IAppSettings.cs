namespace BasketService.Infrastructure.CrossCutting.Configuration
{
    public partial interface IAppSettings
    {
        string LoggingLevel { get; }
    }
}