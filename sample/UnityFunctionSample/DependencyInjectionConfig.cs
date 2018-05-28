using Indigo.Functions.Unity;
using Sample.Storage;
using Unity;

namespace UnityFunctionSample
{
    public class DependencyInjectionConfig : IDependencyConfig
    {
        public void RegisterComponents(UnityContainer container)
        {
            container.RegisterSingleton<ICache, CacheProvider>();
            container.RegisterType<ICacheConfigProvider, CacheConfigProvider>();
            container.RegisterType<IStorageAccess, StorageAccess>();
            container.RegisterType<ITableAccess, CloudTableAccess>();
        }
    }
}
