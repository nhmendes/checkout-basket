namespace BasketService.Infrastructure.CrossCutting.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Tests.Common;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("InfrastructureCrossCuttingTestsCollection")]
    public class InfrastructureCrossCuttingTestsCollection : ICollectionFixture<UnitTestFixture> { }
}