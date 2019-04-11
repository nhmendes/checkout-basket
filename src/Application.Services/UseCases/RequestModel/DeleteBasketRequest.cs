namespace BasketService.Application.Services.UseCases.RequestModel
{
    public class DeleteBasketRequest
    {
        public string BasketId { get; private set; }

        private DeleteBasketRequest() { }

        public static DeleteBasketRequest Create(string basketId)
        {
            return new DeleteBasketRequest
            {
                BasketId = basketId,
            };
        }
    }
}