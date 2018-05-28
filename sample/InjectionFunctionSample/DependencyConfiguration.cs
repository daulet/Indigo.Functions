using Indigo.Functions.Injection;
using Microsoft.Extensions.DependencyInjection;
using Sample.Storage;

namespace InjectionFunctionSample
{
    public class DependencyConfiguration : IDependencyConfiguration
    {
        public void RegisterServices(ServiceCollection collection)
        {
            collection.AddSingleton<ICache, CacheProvider>();
            collection.AddTransient<ICacheConfigProvider, CacheConfigProvider>();
            collection.AddTransient<IStorageAccess, StorageAccess>();
            collection.AddTransient<ITableAccess, CloudTableAccess>();
        }
    }
}
