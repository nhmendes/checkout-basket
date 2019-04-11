namespace BasketService.Presentation.WebApi.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Tests.Common;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [CollectionDefinition("PresentationTestsCollection")]
    public class PresentationTestsCollection : ICollectionFixture<UnitTestFixture> { }
}