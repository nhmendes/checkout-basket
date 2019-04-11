namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    /// <summary>
    /// Error code enum
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Default error code
        /// </summary>
        None = 0,

        /// <summary>
        /// The no results found exception
        /// </summary>
        CancelationBlockingRuleException = 20006,

        /// <summary>
        /// The no results found exception
        /// </summary>
        NoResultsFoundException = 30000,

        /// <summary>
        /// The cannot delete association exception
        /// </summary>
        CannotDeleteAssociationException = 30001,

        /// <summary>
        /// The file type not allowed exception
        /// </summary>
        FileTypeNotAllowedException = 30002,

        /// <summary>
        /// The maximum file size exceeded exception
        /// </summary>
        MaxFileSizeExceededException = 30003,

        /// <summary>
        /// The invalid field exception
        /// </summary>
        InvalidFieldException = 30004,

        /// <summary>
        /// The item already exists exception
        /// </summary>
        ItemAlreadyExistsException = 30005,

        /// <summary>
        /// The missing fields exception
        /// </summary>
        MissingFieldsException = 30006,

        /// <summary>
        /// The invalid operation exception
        /// </summary>
        InvalidOperationException = 30007
    }
}