using Microsoft.Extensions.DependencyInjection;

namespace Indigo.Functions.Injection.IntegrationTests.MisconfiguredTarget
{
    internal class DependencyConfig : IDependencyConfiguration
    {
        public void RegisterServices(ServiceCollection collection)
        {
            collection.AddTransient<IDependency, DependencyImpl>();
        }
    }
}
