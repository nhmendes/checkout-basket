namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    /// <summary>
    /// ApiError class
    ///
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public Error[] Errors { get; set; }
    }
}