using Unity;

namespace Indigo.Functions.Unity.IntegrationTests.MisconfiguredTarget
{
    internal class DependencyConfig : IDependencyConfig
    {
        public void RegisterComponents(UnityContainer container)
        {
            container.RegisterType<IDependency, DependencyImpl>();
        }
    }
}
