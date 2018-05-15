using Autofac;

namespace Indigo.Functions.Autofac.IntegrationTests.Target
{
    public class DependencyConfig : IDependencyConfig
    {
        public void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<DependencyImpl>();
            builder.RegisterType<LoggingDependencyImpl>();
        }
    }
}
