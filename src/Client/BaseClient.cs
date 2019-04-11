namespace BasketService.Client
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Client.HttpConnection;

    public abstract class BaseClient
    {
        public readonly string BaseUri;
        protected IHttpConnection HttpConnection { get; private set; }

        public BaseClient(string baseUri, IHttpConnection httpConnection)
        {
            this.BaseUri = baseUri;
            this.HttpConnection = httpConnection;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            return await this.HttpConnection.GetAsync<T>(new Uri(uri)).ConfigureAwait(false);
        }

        public async Task<T> GetAsync<T>(Uri uri)
        {
            return await this.HttpConnection.GetAsync<T>(uri).ConfigureAwait(false);
        }

        public async Task<TResponse> PostAsync<TBody, TResponse>(Uri uri, TBody body)
        {
            return await this.HttpConnection.PostAsync<TBody, TResponse>(uri, body).ConfigureAwait(false);
        }
    }
}