namespace Farfetch.ReturnContestsService.Data.Repository.Tests.Sources
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoFixture;
    using Farfetch.ReturnContestsService.Data.Repository.MongoClientSetups;
    using Farfetch.ReturnContestsService.Data.Repository.Sources;
    using MongoDB.Driver;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Collection("DataRepositoryTestsCollection")]
    public class SourcesRepositoryTests
    {
        #region Private Members

        private const string SourcesCollection = "Sources";

        private Fixture fixture;

        #endregion Private Members

        #region Ctor

        public SourcesRepositoryTests()
        {
            this.fixture = new Fixture();
        }

        #endregion Ctor

        #region Tests

        [Fact]
        public async Task AddSource_SourceName_AddSuccessfully()
        {
            //Arrange
            var sourcesRepositoryMock = this.ConfigureSourcesRepositoryMock();
            var sourcesRepository = sourcesRepositoryMock.Object;

            var sourceName = this.fixture.Create<string>();
            var expected = sourceName;

            //Act
            var actual = await sourcesRepository.AddSource(sourceName);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllSources_NoParams_GetAllSuccessfully()
        {
            //Arrange
            var sourcesRepositoryMock = this.ConfigureSourcesRepositoryMock();
            var sourcesRepository = sourcesRepositoryMock.Object;

            var sourcesMongoResponse = this.fixture.CreateMany<SourceMongoEntity>();
            var expected = sourcesMongoResponse.Select(s => s.Name);

            var filterGetAllSources = FilterDefinition<SourceMongoEntity>.Empty;

            sourcesRepositoryMock.Setup(sr => sr.CreateEmptyFilter<SourceMongoEntity>())
                                 .Returns(filterGetAllSources);

            sourcesRepositoryMock.Setup(sr => sr.FindByFilter(SourcesCollection, filterGetAllSources))
                                 .ReturnsAsync(sourcesMongoResponse);

            //Act
            var actual = await sourcesRepository.GetAllSources();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateSource_OldSourceNameAndNewSourceName_UpdateSuccessfully()
        {
            //Arrange
            var sourcesRepositoryMock = this.ConfigureSourcesRepositoryMock();
            var sourcesRepository = sourcesRepositoryMock.Object;

            var oldSourceName = this.fixture.Create<string>();
            var newSourceName = this.fixture.Create<string>();
            var expected = newSourceName;

            //Act
            var actual = await sourcesRepository.UpdateSource(oldSourceName, newSourceName);

            //Assert
            Assert.Equal(expected, actual);
        }

        #endregion Tests

        #region Private Methods

        private Mock<IMongoDatabaseClient> CreateMongoDatabaseClient(Mock<IMongoDatabase> mongoDatabaseMock)
        {
            var mongoDatabaseClient = new Mock<IMongoDatabaseClient>();
            mongoDatabaseClient.Setup(x => x.CreateMongoDatabase()).Returns(mongoDatabaseMock.Object);

            return mongoDatabaseClient;
        }

        private void SetupCollectionToMongoDatabaseMock<T>(Mock<IMongoDatabase> mongoDatabaseMock, IMongoCollection<T> collection)
        {
            mongoDatabaseMock.Setup(
               md => md.GetCollection<T>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
           .Returns(collection);
        }

        private Mock<SourcesRepository> CreateSourcesRepositoryMock(IMock<IMongoDatabaseClient> mongoDatabaseClient)
        {
            var mock = new Mock<SourcesRepository>(mongoDatabaseClient.Object)
            {
                CallBase = true
            };

            return mock;
        }

        private Mock<SourcesRepository> ConfigureSourcesRepositoryMock()
        {
            var sourcesCollectionMock = new Mock<IMongoCollection<SourceMongoEntity>>();

            var mongoDatabaseMock = new Mock<IMongoDatabase>();
            this.SetupCollectionToMongoDatabaseMock(mongoDatabaseMock, sourcesCollectionMock.Object);

            var mongoDatabaseClientMock = CreateMongoDatabaseClient(mongoDatabaseMock);

            return this.CreateSourcesRepositoryMock(mongoDatabaseClientMock);
        }

        #endregion Private Methods
    }
}