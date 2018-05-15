using Autofac;

namespace Indigo.Functions.Autofac.IntegrationTests.MisconfiguredTarget
{
    internal class DependencyConfig : IDependencyConfig
    {
        public void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<DependencyImpl>()
                   .As<IDependency>();
        }
    }
}
