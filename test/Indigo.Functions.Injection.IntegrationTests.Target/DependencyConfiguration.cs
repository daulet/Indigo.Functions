using Microsoft.Extensions.DependencyInjection;

namespace Indigo.Functions.Injection.IntegrationTests.Target
{
    public class DependencyConfig : IDependencyConfiguration
    {
        public void RegisterServices(ServiceCollection collection)
        {
            collection.AddTransient<IDependency, DependencyImpl>();
            collection.AddTransient<ILoggingDependency, LoggingDependencyImpl>();
            collection.AddTransient<ValueProvider>();
        }
    }
}
