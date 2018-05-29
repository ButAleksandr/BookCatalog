using System;

namespace BookCatalog.Common.Bootstrap
{
    public interface IServiceProviderFactory
    {
        TService GetService<TService>(params object[] constructParams);

        TService GetMappingService<TService>(string name, params object[] param);

        object GetService(Type serviceType, params object[] constructParams);

        TService TryGetMappingService<TService>(string name, params object[] param);
    }
}
