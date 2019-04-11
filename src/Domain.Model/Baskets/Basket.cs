namespace BasketService.Domain.Model.Baskets
{
    using System;
    using System.Collections.Generic;

    public class Basket
    {
        public string Id { get; }

        public string CustomerEmail { get; }

        public int NumberOfItems => this.items.Count;

        public IEnumerable<Item> Items
        {
            get
            {
                foreach (var pair in this.items)
                {
                    yield return pair.Value;
                }
            }
        }

        private readonly IDictionary<string, Item> items;

        public Basket(string id, string customerEmail)
        {
            this.Id = id;
            this.CustomerEmail = customerEmail;
            this.items = new Dictionary<string, Item>();
        }

        public void AddItem(Item item)
        {
            if (this.items.ContainsKey(item.ItemId))
            {
                throw new ItemAlreadyInTheBasketException();
            }
            this.items.Add(item.ItemId, item);
        }

        public void AddRange(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void RemoveItem(string itemId)
        {
            this.items.Remove(itemId);
        }

        public void UpdateItem(Item item)
        {
            this.items[item.ItemId] = item;
        }

        public static Basket Create(string customerEmail)
        {
            return new Basket(Guid.NewGuid().ToString("N"), customerEmail);
        }
    }
}