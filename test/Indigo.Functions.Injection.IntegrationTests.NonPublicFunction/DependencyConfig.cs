using Unity;

namespace Indigo.Functions.Injection.IntegrationTests.NonPublicFunction
{
    internal class DependencyConfig : IDependencyConfig
    {
        public UnityContainer Container
        {
            get
            {
                var container = new UnityContainer();
                container.RegisterType<IDependency, DependencyImpl>();
                return container;
            }
        }
    }
}
