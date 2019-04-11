namespace ClientTest
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Client.Baskets;
    using BasketService.Client.HttpConnection;

    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();            
        }

        static async Task RunAsync()
        {
            var uri = "http://localhost:49435";
            var token = await GetTokenAsync(uri);

            var client = GetBasketsClient(uri, new Tuple<string, string>("Authorization", token));
            var baskets = await client.GetBasketsAsync();

            foreach (var a in baskets)
            {
                Console.WriteLine(a.Id);
            }
        }

        static async Task<string> GetTokenAsync(string uri)
        {
            var client = GetBasketsAuthorizationClient(uri);
            var token = await client.GetTokenAsync();
            return token;
        }

        static IBasketsClient GetBasketsClient(
            string uri,
            params Tuple<string, string>[] headers)
        {
            var httpConn = new HttpConnection(uri, headers);
            var client = new BasketsClient(httpConn);
            return client;
        }

        static IBasketsAuthorizationClient GetBasketsAuthorizationClient(
            string uri)
        {
            var httpConn = new HttpConnection(uri);
            var client = new BasketsAuthorizationClient(httpConn);
            return client;
        }
    }
}