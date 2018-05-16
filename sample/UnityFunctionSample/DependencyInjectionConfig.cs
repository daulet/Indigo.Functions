using Indigo.Functions.Unity;
using Unity;

namespace UnityFunctionSample
{
    public class DependencyInjectionConfig : IDependencyConfig
    {
        public void RegisterComponents(UnityContainer container)
        {
            container.RegisterType<ITableAccess, CloudTableAccess>();
            container.RegisterType<IStorageAccess, StorageAccess>();
        }
    }
}
