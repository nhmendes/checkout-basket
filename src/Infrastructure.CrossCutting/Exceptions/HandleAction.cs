namespace BasketService.Infrastructure.CrossCutting.Exceptions
{
    /// <summary>
    /// Enum to specify the actions that can be executed over an exception
    /// </summary>
    public enum HandleAction
    {
        /// <summary>
        /// No action will be executed
        /// </summary>
        None = 0,

        /// <summary>
        /// Writes into log the exceptions details.
        /// </summary>
        SendToLog = 1
    }
}
