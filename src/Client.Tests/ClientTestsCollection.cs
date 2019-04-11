namespace BasketService.Client.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Tests.Common;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("ClientTestsCollection")]
    public class ClientTestsCollection : ICollectionFixture<UnitTestFixture> { }
}