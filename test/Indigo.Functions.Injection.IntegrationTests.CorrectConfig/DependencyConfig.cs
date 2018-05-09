using Unity;

namespace Indigo.Functions.Injection.IntegrationTests.CorrectConfig
{
    public class DependencyConfig : IDependencyConfig
    {
        public UnityContainer Container
        {
            get
            {
                var container = new UnityContainer();
                container.RegisterType<IDependency, DependencyImpl>();
                container.RegisterType<ILoggingDependency, LoggingDependencyImpl>();
                return container;
            }
        }
    }
}
