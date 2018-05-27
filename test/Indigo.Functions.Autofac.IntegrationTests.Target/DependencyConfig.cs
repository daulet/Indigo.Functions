using Autofac;

namespace Indigo.Functions.Autofac.IntegrationTests.Target
{
    public class DependencyConfig : IDependencyConfig
    {
        public void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<DependencyImpl>()
                   .As<IDependency>();
            builder.RegisterType<LoggingDependencyImpl>()
                   .As<ILoggingDependency>();
            builder.RegisterType<ValueProvider>();
        }
    }
}
