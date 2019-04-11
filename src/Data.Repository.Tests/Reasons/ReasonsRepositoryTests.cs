namespace Farfetch.ReturnContestsService.Data.Repository.Tests.Reasons
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoFixture;
    using Farfetch.ReturnContestsService.Data.Repository.MongoClientSetups;
    using Farfetch.ReturnContestsService.Data.Repository.Reasons;
    using Farfetch.ReturnContestsService.Domain.Model.Reasons;
    using MongoDB.Driver;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Data.Repository", "ReasonsRepository")]
    [Collection("DataRepositoryTestsCollection")]
    public class ReasonsRepositoryTests
    {
        #region Private Members

        private const string ReasonsCollection = "Reasons";
        private const string ReasonsFiltersCollection = "ReasonsFilters";

        private Fixture fixture;

        #endregion Private Members

        #region Ctor

        public ReasonsRepositoryTests()
        {
            this.fixture = new Fixture();
        }

        #endregion Ctor

        #region Tests

        [Fact]
        public async Task GetReasons_NoParams_NoResults()
        {
            //Arrange
            var reasonsRepositoryMock = this.ConfigureReasonsRepositoryMock();
            var reasonsRepository = reasonsRepositoryMock.Object;

            var results = new List<Reason>();
            var expected = new List<Reason>();

            reasonsRepositoryMock.Setup(rr => rr.FindAll<Reason>(ReasonsCollection))
                                 .ReturnsAsync(results);

            //Act
            var actual = await reasonsRepository.GetAll();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetReasons_Tag_CorrectResults()
        {
            //Arrange
            var reasonsRepositoryMock = this.ConfigureReasonsRepositoryMock();
            var reasonsRepository = reasonsRepositoryMock.Object;
            var tags = new List<string>() { "SAMEDAY" };

            var expected = this.fixture.Build<Reason>().CreateMany(1);
            var expectedReasonFilter = this.fixture.Build<ReasonFilter>()
                                     .With(rf => rf.ReasonsIds, new List<string>() { expected.First().Id, "anotherRandomId" })
                                     .CreateMany(1);

            var filterReasonFilter = Builders<ReasonFilter>.Filter.Empty & Builders<ReasonFilter>.Filter.Eq(rf => rf.Tags, tags);
            var filterReasons = Builders<Reason>.Filter.In(r => r.Id, expectedReasonFilter.First().ReasonsIds);

            var reasonsQuery = new ReasonsQuery()
            {
                Tags = tags
            };

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsFilter(reasonsQuery))
                                 .Returns(filterReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsFiltersCollection, filterReasonFilter))
                                 .ReturnsAsync(expectedReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsByIds(expectedReasonFilter.First().ReasonsIds))
                                 .Returns(filterReasons);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsCollection, filterReasons))
                                 .ReturnsAsync(expected);

            //Act
            var actual = await reasonsRepository.Get(reasonsQuery);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetReasons_WorkflowStatus_CorrectResults()
        {
            //Arrange
            var reasonsRepositoryMock = this.ConfigureReasonsRepositoryMock();
            var reasonsRepository = reasonsRepositoryMock.Object;
            var workflowStatus = "Step_1";

            var expected = this.fixture.Build<Reason>().CreateMany(1);
            var expectedReasonFilter = this.fixture.Build<ReasonFilter>()
                                     .With(rf => rf.ReasonsIds, new List<string>() { expected.First().Id, "anotherRandomId" })
                                     .CreateMany(1);

            var filterReasonFilter = Builders<ReasonFilter>.Filter.Empty & Builders<ReasonFilter>.Filter.Eq(rf => rf.WorkflowStatusCode, workflowStatus);
            var filterReasons = Builders<Reason>.Filter.In(r => r.Id, expectedReasonFilter.First().ReasonsIds);

            var reasonsQuery = new ReasonsQuery()
            {
                WorkflowStatus = workflowStatus
            };

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsFilter(reasonsQuery))
                                 .Returns(filterReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsFiltersCollection, filterReasonFilter))
                                 .ReturnsAsync(expectedReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsByIds(expectedReasonFilter.First().ReasonsIds))
                                 .Returns(filterReasons);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsCollection, filterReasons))
                                 .ReturnsAsync(expected);

            //Act
            var actual = await reasonsRepository.Get(reasonsQuery);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetReasons_TenantId_CorrectResults()
        {
            //Arrange
            var reasonsRepositoryMock = this.ConfigureReasonsRepositoryMock();
            var reasonsRepository = reasonsRepositoryMock.Object;
            var tenantId = 10000;

            var expected = this.fixture.Build<Reason>().CreateMany(1);
            var expectedReasonFilter = this.fixture.Build<ReasonFilter>()
                                     .With(rf => rf.ReasonsIds, new List<string>() { expected.First().Id, "anotherRandomId" })
                                     .CreateMany(1);

            var filterReasonFilter = Builders<ReasonFilter>.Filter.Empty & Builders<ReasonFilter>.Filter.Eq(rf => rf.TenantId, tenantId);
            var filterReasons = Builders<Reason>.Filter.In(r => r.Id, expectedReasonFilter.First().ReasonsIds);

            var reasonsQuery = new ReasonsQuery()
            {
                TenantId = tenantId
            };

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsFilter(reasonsQuery))
                                 .Returns(filterReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsFiltersCollection, filterReasonFilter))
                                 .ReturnsAsync(expectedReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsByIds(expectedReasonFilter.First().ReasonsIds))
                                 .Returns(filterReasons);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsCollection, filterReasons))
                                 .ReturnsAsync(expected);

            //Act
            var actual = await reasonsRepository.Get(reasonsQuery);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetReasons_ClientId_CorrectResults()
        {
            //Arrange
            var reasonsRepositoryMock = this.ConfigureReasonsRepositoryMock();
            var reasonsRepository = reasonsRepositoryMock.Object;
            var clientId = "Client_1";

            var expected = this.fixture.Build<Reason>().CreateMany(1);
            var expectedReasonFilter = this.fixture.Build<ReasonFilter>()
                                     .With(rf => rf.ReasonsIds, new List<string>() { expected.First().Id, "anotherRandomId" })
                                     .CreateMany(1);

            var filterReasonFilter = Builders<ReasonFilter>.Filter.Empty & Builders<ReasonFilter>.Filter.Eq(rf => rf.ClientId, clientId);
            var filterReasons = Builders<Reason>.Filter.In(r => r.Id, expectedReasonFilter.First().ReasonsIds);

            var reasonsQuery = new ReasonsQuery()
            {
                ClientId = clientId
            };

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsFilter(reasonsQuery))
                                 .Returns(filterReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsFiltersCollection, filterReasonFilter))
                                 .ReturnsAsync(expectedReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsByIds(expectedReasonFilter.First().ReasonsIds))
                                 .Returns(filterReasons);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsCollection, filterReasons))
                                 .ReturnsAsync(expected);

            //Act
            var actual = await reasonsRepository.Get(reasonsQuery);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetReasons_ReasonsQueryAllParameters_CorrectResults()
        {
            //Arrange
            var reasonsRepositoryMock = this.ConfigureReasonsRepositoryMock();
            var reasonsRepository = reasonsRepositoryMock.Object;

            var tenantId = 10000;
            var clientId = "Client_1";
            var workflowStatus = "Step_1";
            var tags = new List<string>() { "SAMEDAY" };

            var expected = this.fixture.Build<Reason>().CreateMany(1);
            var expectedReasonFilter = this.fixture.Build<ReasonFilter>()
                                    .With(rf => rf.ReasonsIds, new List<string>() { expected.First().Id, "anotherRandomId" })
                                    .CreateMany(1);

            var filterReasonFilter = Builders<ReasonFilter>.Filter.Eq(rf => rf.TenantId, tenantId)
                                    & Builders<ReasonFilter>.Filter.Eq(rf => rf.ClientId, clientId)
                                    & Builders<ReasonFilter>.Filter.Eq(rf => rf.WorkflowStatusCode, workflowStatus)
                                    & Builders<ReasonFilter>.Filter.Eq(rf => rf.Tags, tags);

            var filterReasons = Builders<Reason>.Filter.In(r => r.Id, expectedReasonFilter.First().ReasonsIds);

            var reasonsQuery = new ReasonsQuery()
            {
                TenantId = tenantId,
                ClientId = clientId,
                WorkflowStatus = workflowStatus,
                Tags = tags
            };

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsFilter(reasonsQuery))
                                 .Returns(filterReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsFiltersCollection, filterReasonFilter))
                                 .ReturnsAsync(expectedReasonFilter);

            reasonsRepositoryMock.Setup(rr => rr.GetFilterForReasonsByIds(expectedReasonFilter.First().ReasonsIds))
                                 .Returns(filterReasons);

            reasonsRepositoryMock.Setup(rr => rr.FindByFilter(ReasonsCollection, filterReasons))
                                 .ReturnsAsync(expected);

            //Act
            var actual = await reasonsRepository.Get(reasonsQuery);

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

        private Mock<ReasonsRepository> CreateReasonsRepositoryMock(IMock<IMongoDatabaseClient> mongoDatabaseClient)
        {
            var mock = new Mock<ReasonsRepository>(mongoDatabaseClient.Object)
            {
                CallBase = true
            };

            return mock;
        }

        private Mock<ReasonsRepository> ConfigureReasonsRepositoryMock()
        {
            var reasonsCollectionMock = new Mock<IMongoCollection<Reason>>();
            var reasonsFiltersCollectionMock = new Mock<IMongoCollection<ReasonFilter>>();

            var mongoDatabaseMock = new Mock<IMongoDatabase>();
            this.SetupCollectionToMongoDatabaseMock(mongoDatabaseMock, reasonsCollectionMock.Object);
            this.SetupCollectionToMongoDatabaseMock(mongoDatabaseMock, reasonsFiltersCollectionMock.Object);

            var mongoDatabaseClientMock = CreateMongoDatabaseClient(mongoDatabaseMock);

            return this.CreateReasonsRepositoryMock(mongoDatabaseClientMock);
        }

        #endregion Private Methods
    }
}