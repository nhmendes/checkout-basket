namespace BasketService.Application.Services.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Tests.Common;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("ApplicationServicesTestsCollection")]
    public class ApplicationServicesTestsCollection : ICollectionFixture<UnitTestFixture> { }
}