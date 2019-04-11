namespace BasketService.Data.Repository.Baskets
{
    using System.Collections.Generic;
    using BasketService.Domain.Core.RepositoryInterfaces;
    using BasketService.Domain.Model.Baskets;

    public class MockDatabase : IBasketsDatabase
    {
        public IDictionary<string, Basket> Baskets { get; set; }

        public MockDatabase()
        {
            this.Baskets = new Dictionary<string, Basket>();
        }
    }
}