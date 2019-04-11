namespace BasketService.Infrastructure.CrossCutting.Tests.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using AutoFixture;
    using BasketService.Infrastructure.CrossCutting.Exceptions;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Trait("Infrastructure.CrossCutting", "Exceptions")]
    [Collection("InfrastructureCrossCuttingTestsCollection")]
    public class ExceptionHandlerTests
    {
        #region Private Members

        private Fixture fixture;

        #endregion Private Members

        #region Ctor

        public ExceptionHandlerTests()
        {
            this.fixture = new Fixture();
        }

        #endregion Ctor

        #region Tests

        [Fact]
        public void ExceptionHandler_HandleException_LogVerified()
        {
            //Arrange
            var logMock = new Mock<ILog>();
            var exception = new Exception();

            //Act
            ExceptionHandler.HandleException(exception,logMock.Object);

            //Assert
            logMock.Verify(l => l.Error(It.IsAny<string>()), Times.Once());
        }

        #endregion
    }
}