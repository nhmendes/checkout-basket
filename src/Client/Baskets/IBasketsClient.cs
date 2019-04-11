namespace BasketService.Client.Baskets
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BasketService.Application.DTO.Baskets;

    public interface IBasketsClient
    {
        Task<Basket> GetBasketAsync(string id);
        
        Task<IEnumerable<Basket>> GetBasketsAsync();
    }
}