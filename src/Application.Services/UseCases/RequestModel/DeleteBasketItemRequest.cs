namespace BasketService.Application.Services.UseCases.RequestModel
{
    public class DeleteBasketItemRequest
    {
        public string BasketId { get; private set; }
        public string ItemId { get; private set; }

        private DeleteBasketItemRequest() { }

        public static DeleteBasketItemRequest Create(string basketId, string itemId)
        {
            return new DeleteBasketItemRequest
            {
                BasketId = basketId,
                ItemId = itemId
            };
        }
    }
}