namespace BasketService.Client.HttpConnection
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class HttpConnection : IHttpConnection
    {
        private readonly string baseAddress;
        private readonly Tuple<string, string>[] headers;

        public HttpConnection(string baseAddress, params Tuple<string, string>[] headers)
        {
            this.baseAddress = baseAddress;
            this.headers = headers;
        }

        public string BaseAddress => this.baseAddress;

        public async Task<T> GetAsync<T>(Uri uri)
        {
            T result = default;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var header in this.headers)
                {
                    client.DefaultRequestHeaders.Add(header.Item1, header.Item2);
                }

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }

            return result;
        }

        public async Task<TResponse> PostAsync<TBody, TResponse>(Uri uri, TBody body)
        {
            TResponse result = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseAddress);
                string json = JsonConvert.SerializeObject(body);
                var response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
                }
            }
            return result;
        }
    }
}