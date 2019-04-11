namespace BasketService.Application.Services.UseCases.RequestModel
{
    /// <summary>
    /// Represents a request to the 'GetBasketsRequest' usecase
    /// </summary>
    public class GetAllBasketsRequest
    {
        /// <summary>
        /// Initializes a new instance of the GetBasketsRequest class
        /// </summary>
        private GetAllBasketsRequest() { }

        /// <summary>
        /// Returns an instance of GetBasketsRequest
        /// </summary>
        /// <param name="basketId">The basket identifier</param>
        /// <returns></returns>
        public static GetAllBasketsRequest Create()
        {
            return new GetAllBasketsRequest();
        }
    }
}