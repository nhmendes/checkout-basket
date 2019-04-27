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
            return await Task.Run(() =>
            {
                this.mockDatabase.Baskets.TryGetValue(id, out Basket basket);
                return basket;
            });
        }

        public async Task<IEnumerable<Basket>> GetAll()
        {
            return await Task.Run(() => mockDatabase.Baskets.Values);
        }

        public async Task Insert(Basket basket)
        {
            await Task.Run(() => this.mockDatabase.Baskets.Add(basket.Id, basket));
        }

        public async Task DeleteItem(string basketId, string itemId)
        {
            await Task.Run(() =>
            {
                if (this.mockDatabase.Baskets.ContainsKey(basketId))
                {
                    this.mockDatabase.Baskets[basketId].RemoveItem(itemId);
                }
            });
        }

        public async Task Delete(string basketId)
        {
            await Task.Run(() => this.mockDatabase.Baskets.Remove(basketId));
        }

        public async Task<Item> InsertItem(string basketId, Item item)
        {
            return await Task.Run(() =>
            {
                this.mockDatabase.Baskets[basketId].AddItem(item);
                return item;
            });
        }

        public async Task<IEnumerable<Item>> InsertItems(string basketId, IEnumerable<Item> items)
        {
            return await Task.Run(() =>
            {
                this.mockDatabase.Baskets[basketId].AddRange(items);
                return items;
            });
        }

        public async Task UpdateItem(string basketId, Item domainItem)
        {
            await Task.Run(() => this.mockDatabase.Baskets[basketId].UpdateItem(domainItem));
        }
    }
}