namespace BasketService.Client.Baskets
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Client.HttpConnection;

    public class BasketsAuthorizationClient : BaseClient, IBasketsAuthorizationClient
    {
        private const string controller = "BasketAuthorization";

        public BasketsAuthorizationClient(IHttpConnection httpConnection)
            : base(httpConnection.BaseAddress, httpConnection)
        {
        }

        public async Task<string> GetTokenAsync()
        {
            var path = $"{this.BaseUri}/{controller}/connect/token";
            return await this.GetAsync<string>(new Uri(path));
        }
    }
}