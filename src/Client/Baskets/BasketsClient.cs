[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("BasketService.Client.Tests")]

namespace BasketService.Client.Baskets
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Client.HttpConnection;

    public class BasketsClient : BaseClient, IBasketsClient
    {
        private const string controller = "Baskets";

        public BasketsClient(IHttpConnection httpConnection)
            : base(httpConnection.BaseAddress, httpConnection)
        {
        }

        public async Task<Basket> GetBasketAsync(string id)
        {
            var path = $"{this.BaseUri}/{controller}/{id}";
            return await this.GetAsync<Basket>(new Uri(path));
        }

        public async Task<IEnumerable<Basket>> GetBasketsAsync()
        {
            var path = $"{this.BaseUri}/{controller}";
            return await this.GetAsync<IEnumerable<Basket>>(new Uri(path));
        }
    }
}