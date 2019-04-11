namespace BasketService.Domain.Core.RepositoryInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BasketService.Domain.Model.Baskets;

    public interface IBasketsRepository
    {
        Task<IEnumerable<Basket>> GetAll();

        Task<Basket> Get(string id);

        Task Insert(Basket basket);

        Task<Item> InsertItem(string basketId, Item item);

        Task<IEnumerable<Item>> InsertItems(string basketId, IEnumerable<Item> items);

        Task Delete(string basketId);

        Task DeleteItem(string basketId, string itemId);

        Task UpdateItem(string basketId, Item domainItem);
    }
}