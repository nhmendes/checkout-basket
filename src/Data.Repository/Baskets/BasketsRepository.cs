namespace BasketService.Data.Repository.Baskets
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Domain.Model.Baskets;

    public class BasketsRepository : IBasketsRepository
    {
        private readonly IBasketsDatabase mockDatabase;

        public BasketsRepository(IBasketsDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public async Task<Basket> Get(string id)
        {
            this.mockDatabase.Baskets.TryGetValue(id, out Basket basket);
            return basket;
        }

        public async Task<IEnumerable<Basket>> GetAll()
        {
            return mockDatabase.Baskets.Values;
        }

        public async Task Insert(Basket basket)
        {
            this.mockDatabase.Baskets.Add(basket.Id, basket);
        }

        public async Task DeleteItem(string basketId, string itemId)
        {
            if (this.mockDatabase.Baskets.ContainsKey(basketId))
            {
                this.mockDatabase.Baskets[basketId].RemoveItem(itemId);
            }
        }

        public async Task Delete(string basketId)
        {
            this.mockDatabase.Baskets.Remove(basketId);
        }

        public async Task<Item> InsertItem(string basketId, Item item)
        {
            this.mockDatabase.Baskets[basketId].AddItem(item);
            return item;
        }

        public async Task<IEnumerable<Item>> InsertItems(string basketId, IEnumerable<Item> items)
        {
            this.mockDatabase.Baskets[basketId].AddRange(items);
            return items;
        }

        public async Task UpdateItem(string basketId, Item domainItem)
        {
            this.mockDatabase.Baskets[basketId].UpdateItem(domainItem);
        }
    }
}