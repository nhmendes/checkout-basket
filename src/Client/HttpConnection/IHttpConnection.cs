namespace BasketService.Client.HttpConnection
{
    using System;
    using System.Threading.Tasks;

    public interface IHttpConnection
    {
        string BaseAddress { get; }
        Task<T> GetAsync<T>(Uri uri);
        Task<TResponse> PostAsync<TBody, TResponse>(Uri uri, TBody body);
    }
}