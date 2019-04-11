namespace BasketService.Application.Services.UseCases.RequestModel
{
    public class GetBasketItemByIdRequest
    {
        public string BasketId { get; private set; }
        public string ItemId { get; private set; }

        private GetBasketItemByIdRequest() { }

        public static GetBasketItemByIdRequest Create(string basketId, string itemId)
        {
            return new GetBasketItemByIdRequest
            {
                BasketId = basketId,
                ItemId = itemId
            };
        }
    }
}