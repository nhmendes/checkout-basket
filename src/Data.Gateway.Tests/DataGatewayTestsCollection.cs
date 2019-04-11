namespace BasketService.Data.Gateway.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Tests.Common;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("DataRepositoryTestsCollection")]
    public class DataGatewayTestsCollection : ICollectionFixture<UnitTestFixture> { }
}