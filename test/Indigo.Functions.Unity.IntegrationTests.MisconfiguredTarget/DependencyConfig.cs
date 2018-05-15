using Unity;

namespace Indigo.Functions.Unity.IntegrationTests.MisconfiguredTarget
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
