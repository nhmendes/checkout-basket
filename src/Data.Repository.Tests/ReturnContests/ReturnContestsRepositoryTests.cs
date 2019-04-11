namespace Farfetch.ReturnContestsService.Data.Repository.Tests.ReturnContests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AutoFixture;
    using Farfetch.ReturnContestsService.Data.Repository.MongoClientSetups;
    using Farfetch.ReturnContestsService.Data.Repository.ReturnContests;
    using Farfetch.ReturnContestsService.Domain.Model.Attachments;
    using Farfetch.ReturnContestsService.Domain.Model.ReturnContests;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Data.Repository", "ReturnContestsRepository")]
    [Collection("DataRepositoryTestsCollection")]
    public class ReturnContestsRepositoryTests
    {
        #region Private Members

        private const string ReturnContestsCollection = "ReturnContests";

        private Fixture fixture;

        #endregion Private Members

        #region Ctor

        public ReturnContestsRepositoryTests()
        {
            this.fixture = new Fixture();
        }

        #endregion Ctor

        #region Tests

        [Fact]
        public async Task GetReturnContests_NoParams_NoResults()
        {
            //Arrange
            var contestsRepositoryMock = this.ConfigureReturnContestsRepositoryMock();
            var contestsRepository = contestsRepositoryMock.Object;

            var results = new List<ReturnContest>();
            var expected = new List<ReturnContest>();

            contestsRepositoryMock.Setup(rr => rr.FindAll<ReturnContest>(ReturnContestsCollection))
                                   .ReturnsAsync(results);

            //Act
            var actual = await contestsRepository.GetReturnContests();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetReturnContestByReturnId_ReturnId_NoResults()
        {
            //Arrange
            var contestsRepositoryMock = this.ConfigureReturnContestsRepositoryMock();
            var contestsRepository = contestsRepositoryMock.Object;

            var returnId = 123456;
            var results = new List<ReturnContest>();
            var expected = new List<ReturnContest>();

            var filterByReturnId = Builders<ReturnContest>.Filter.Eq(rc => rc.ReturnId, returnId);
            contestsRepositoryMock.Setup(cr => cr.GetFilterByReturnId(returnId))
                                .Returns(filterByReturnId);
            contestsRepositoryMock.Setup(cr => cr.FindByFilter(ReturnContestsCollection, filterByReturnId))
                                .ReturnsAsync(expected);

            //Act
            var actual = await contestsRepository.GetReturnContestByReturnId(returnId);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAttachment_ContestIdAndFileId_NoResults()
        {
            //Arrange
            var contestsRepositoryMock = this.ConfigureReturnContestsRepositoryMock();
            var contestsRepository = contestsRepositoryMock.Object;

            var contestId = "AAA123";
            var fileId = "BBB321";

            var document = new BsonDocument();
            var expected = new Attachment();

            contestsRepositoryMock.Setup(cr => cr.IsValidObjectIdFormat(contestId, fileId))
                                .Returns(true);

            var filter = Builders<ReturnContest>.Filter.Empty;
            contestsRepositoryMock.Setup(cr => cr.GetFilterByIdAndAttachmentId(contestId, fileId))
                                .Returns(filter);

            var projection = Builders<ReturnContest>.Projection.Combine();
            contestsRepositoryMock.Setup(cr => cr.GetAttachmentsIdProjection())
                                .Returns(projection);

            contestsRepositoryMock.Setup(cr => cr.FindByFilterAndProjection(ReturnContestsCollection, filter, projection))
                                .ReturnsAsync(document);

            contestsRepositoryMock.Setup(cr => cr.BsonDocumentToAttachment(document))
                                .Returns(expected);

            //Act
            var actual = await contestsRepository.GetAttachment(contestId, fileId);

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

        private Mock<ReturnContestsRepository> CreateReasonsRepositoryMock(IMock<IMongoDatabaseClient> mongoDatabaseClient)
        {
            var mock = new Mock<ReturnContestsRepository>(mongoDatabaseClient.Object)
            {
                CallBase = true
            };

            return mock;
        }

        private Mock<ReturnContestsRepository> ConfigureReturnContestsRepositoryMock()
        {
            var contestsCollectionMock = new Mock<IMongoCollection<ReturnContest>>();

            var mongoDatabaseMock = new Mock<IMongoDatabase>();

            mongoDatabaseMock.Setup(
                     md => md.GetCollection<ReturnContest>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                             .Returns(contestsCollectionMock.Object);

            var mongoDatabaseClientMock = CreateMongoDatabaseClient(mongoDatabaseMock);

            return this.CreateReasonsRepositoryMock(mongoDatabaseClientMock);
        }

        #endregion Private Methods
    }
}