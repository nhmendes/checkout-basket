using System;

namespace BasketService.Domain.Model.Baskets
{
    public class Item
    {
        public string ItemId { get; private set; }
        public string Variant { get; private set; }
        public int Quantity { get; private set; }

        private Item(string itemId, string variant, int quantity)
        {
            this.ItemId = itemId;
            this.Variant = variant;
            this.Quantity = quantity;
        }

        public void SetQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

        public static Item Create(string variant, int quantity)
        {
            return new Item(Guid.NewGuid().ToString("N"), variant, quantity);
        }

        public static Item Create(string itemId, string variant, int quantity)
        {
            return new Item(itemId, variant, quantity);
        }
    }
}