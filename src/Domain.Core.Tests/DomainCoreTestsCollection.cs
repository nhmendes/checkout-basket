namespace BasketService.Domain.Core.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Tests.Common;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("DomainCoreTestsCollection")]
    public class DomainCoreTestsCollection : ICollectionFixture<UnitTestFixture> { }
}