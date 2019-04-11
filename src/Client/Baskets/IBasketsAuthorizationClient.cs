namespace BasketService.Client.Baskets
{
    using System.Threading.Tasks;

    public interface IBasketsAuthorizationClient
    {
        Task<string> GetTokenAsync();
    }
}