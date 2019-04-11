namespace BasketService.Domain.Model.Baskets
{
    using System;

    public class ItemAlreadyInTheBasketException : Exception
    {
        public ItemAlreadyInTheBasketException()
           : base("Item already added to the basket")
        {
        }
    }
}