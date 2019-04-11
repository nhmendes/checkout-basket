namespace BasketService.Application.DTO.Baskets
{
    using System.Collections.Generic;

    public class Basket
    {
        public string Id { get; set; }

        public string CustomerEmail { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}