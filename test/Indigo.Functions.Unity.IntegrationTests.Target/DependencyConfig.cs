using Unity;

namespace Indigo.Functions.Unity.IntegrationTests.Target
{
    public class DependencyConfig : IDependencyConfig
    {
        public void RegisterComponents(UnityContainer container)
        {
            container.RegisterType<IDependency, DependencyImpl>();
            container.RegisterType<ILoggingDependency, LoggingDependencyImpl>();
        }
    }
}
