namespace BasketService.Domain.Core.RepositoryInterfaces
{
    using System.Collections.Generic;
    using BasketService.Domain.Model.Baskets;

    public interface IBasketsDatabase
    {
        IDictionary<string, Basket> Baskets { get; }
    }
}