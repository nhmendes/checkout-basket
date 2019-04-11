namespace BasketService.Domain.Services.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Tests.Common;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("DomainServiceTestsCollection")]
    public class DomainServiceTestsCollection : ICollectionFixture<UnitTestFixture> { }
}