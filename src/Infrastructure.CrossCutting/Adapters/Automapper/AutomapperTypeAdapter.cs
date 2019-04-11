namespace BasketService.Infrastructure.CrossCutting.Adapters.Automapper
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;

    /// <summary>
    /// <c>Automapper</c> type adapter implementation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AutomapperTypeAdapter
       : ITypeAdapter
    {
        #region ITypeAdapter Members

        /// <summary>
        /// Adapt a source object to an instance of type <c>"TTarget"</c>/&gt;
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns>
        ///   <paramref name="source" /> <typeparamref name="TTarget" />mapped to
        /// </returns>
        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// Adapt a source object to an instance of type <c>"TTarget"</c>/&gt;
        /// </summary>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns>
        ///   <paramref name="source" /> mapped to <typeparamref name="TTarget" />mapped to
        /// </returns>
        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return Mapper.Map<TTarget>(source);
        }

        #endregion ITypeAdapter Members
    }
}