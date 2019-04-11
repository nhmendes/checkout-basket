namespace BasketService.Application.Services.UseCases.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an immutable action. This action MUST NOT mutate the state of the system (read-only).
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TAction">The result of the query action</typeparam>
    public interface IQueryAction<in TRequest, TAction>
    {
        /// <summary>
        /// Executes a read-only action. This action MUST NOT mutate the state of the system (read-only).
        /// </summary>
        /// <param name="request">The request type</param>
        /// <returns>Returns the result of the command action</returns>
        Task<TAction> Execute(TRequest request);
    }
}