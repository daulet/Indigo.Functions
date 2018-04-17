using Indigo.Functions.Injection;
using Unity;

namespace DependencyInjectionSample
{
    public class DependencyInjectionConfig : IDependencyConfig
    {
        public UnityContainer Container
        {
            get
            {
                var container = new UnityContainer();
                container.RegisterType<ITableAccess, CloudTableAccess>();
                container.RegisterType<IStorageAccess, StorageAccess>();
                return container;
            }
        }
    }
}
