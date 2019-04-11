namespace BasketService.Application.Services.UseCases.RequestModel
{
    public class InsertBasketItemRequest
    {
        public string BasketId { get; private set; }
        public string ItemVariant { get; private set; }
        public int Quantity { get; private set; }

        private InsertBasketItemRequest() { }

        public static InsertBasketItemRequest Create(string basketId, string itemVariant, int quantity)
        {
            return new InsertBasketItemRequest
            {
                BasketId = basketId,
                ItemVariant = itemVariant,
                Quantity = quantity
            };
        }
    }
}