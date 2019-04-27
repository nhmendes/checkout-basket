namespace BasketService.Data.Repository.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("DataRepositoryTestsCollection")]
    public class DataRepositoryTestsCollection : ICollectionFixture<UnitTestFixture> { }
}