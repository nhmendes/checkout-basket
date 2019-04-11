namespace BasketService.Application.Services.UseCases.RequestModel
{
    public class InsertBasketRequest
    {
        public string CustomerEmail { get; private set; }

        private InsertBasketRequest() { }

        public static InsertBasketRequest Create(string customerEmail)
        {
            return new InsertBasketRequest
            {
                CustomerEmail = customerEmail
            };
        }
    }
}