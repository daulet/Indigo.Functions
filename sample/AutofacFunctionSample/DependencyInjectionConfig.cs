using Autofac;
using Indigo.Functions.Autofac;

namespace AutofacFunctionSample
{
    public class DependencyInjectionConfig : IDependencyConfig
    {
        public void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<CacheProvider>().As<ICache>().SingleInstance();
            builder.RegisterType<CacheConfigProvider>().As<ICacheConfigProvider>();
            builder.RegisterType<StorageAccess>().As<IStorageAccess>();
            builder.RegisterType<CloudTableAccess>().As<ITableAccess>();
        }
    }
}
