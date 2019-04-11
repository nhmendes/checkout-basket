namespace BasketService.Application.Services.UseCases.RequestModel
{
    /// <summary>
    /// Represents a request to the 'GetBasketsRequest' usecase
    /// </summary>
    public class GetBasketsByIdRequest
    {
        /// <summary>
        /// The basket identifier
        /// </summary>
        public string BasketId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the GetBasketsRequest class
        /// </summary>
        private GetBasketsByIdRequest() { }

        /// <summary>
        /// Returns an instance of GetBasketsRequest
        /// </summary>
        /// <param name="basketId">The basket identifier</param>
        /// <returns></returns>
        public static GetBasketsByIdRequest Create(string basketId)
        {
            return new GetBasketsByIdRequest
            {
                BasketId = basketId,
            };
        }
    }
}