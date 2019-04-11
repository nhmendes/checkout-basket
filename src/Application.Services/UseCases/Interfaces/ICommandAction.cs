namespace BasketService.Application.Services.UseCases.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an mutable action. This action mutates the state of the system (create, update, delete).
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TAction">The result of the command action</typeparam>
    public interface ICommandAction<in TRequest, TAction>
    {
        /// <summary>
        /// Executes an actions thay mutates the system.
        /// </summary>
        /// <param name="request">The request type</param>
        /// <returns>Returns the result of the command action</returns>
        Task<TAction> Execute(TRequest request);
    }

    public interface ICommandAction<in TRequest>
    {
        /// <summary>
        /// Executes an actions thay mutates the system.
        /// </summary>
        /// <param name="request">The request type</param>
        Task Execute(TRequest request);
    }
}