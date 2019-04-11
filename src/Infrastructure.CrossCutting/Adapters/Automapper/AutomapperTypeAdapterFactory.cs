namespace BasketService.Infrastructure.CrossCutting.Adapters.Automapper
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;

    [ExcludeFromCodeCoverage]
    public class AutomapperTypeAdapterFactory : ITypeAdapterFactory
    {
        #region Constructor

        /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory()
        {
            //var profiles = AppDomain.CurrentDomain
            //                .GetAssemblies()
            //                .Where(x => x.FullName.StartsWith("Farfetch"))
            //                .SelectMany(a => a.GetTypes())
            //                .Where(t => t.BaseType == typeof(Profile))
            //                .Where(t => t.FullName != "AutoMapper.SelfProfiler`2")
            //                .ToList();

            //try
            //{
            //    Mapper.Initialize(cfg =>
            //    {
            //        foreach (var item in profiles)
            //        {
            //            cfg.AddProfile(Activator.CreateInstance(item) as Profile);
            //        }
            //    });
            //}
            //catch (ReflectionTypeLoadException ex)
            //{
            //    if (ex.LoaderExceptions != null)
            //    {
            //        throw;
            //    }
            //}

            //// Validate mappings
            //Mapper.AssertConfigurationIsValid();
        }

        #endregion Constructor

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter();
        }

        #endregion ITypeAdapterFactory Members
    }
}