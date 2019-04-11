namespace BasketService.Application.Services.UseCases.RequestModel
{
    using BasketService.Application.DTO.Baskets;

    public class UpdateBasketItemRequest
    {
        public string BasketId { get; private set; }

        public Item Item { get; private set; }

        private UpdateBasketItemRequest() { }

        public static UpdateBasketItemRequest Create(string basketId, Item item)
        {
            return new UpdateBasketItemRequest
            {
                BasketId = basketId,
                Item = item
            };
        }
    }
}