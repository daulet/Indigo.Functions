using Indigo.Functions.Injection.Internal;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Indigo.Functions.Injection
{
    public class InjectExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<InjectAttribute>();

            rule.BindToInput<Anonymous>((attribute) => null);

            var dependencyConfig = InitializeContainer(context);
            if (dependencyConfig != null)
            {
                var serviceCollection = new ServiceCollection();
                dependencyConfig.RegisterServices(serviceCollection);

                var configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();
                serviceCollection.AddSingleton<IConfiguration>(configuration);

                var logger = context.Config.LoggerFactory.CreateLogger("Host.General");
                serviceCollection.AddSingleton(logger);

                var container = serviceCollection.BuildServiceProvider();
                rule.AddOpenConverter<Anonymous, OpenType>(typeof(InjectConverter<>), context.Config, container);
            }
        }

        private static IDependencyConfiguration InitializeContainer(ExtensionConfigContext context)
        {
            var configType = context.Config.TypeLocator.GetTypes()
                .Where(x => typeof(IDependencyConfiguration).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .FirstOrDefault();

            IDependencyConfiguration configuration = null;
            if (configType != null)
            {
                var configInstance = Activator.CreateInstance(configType);
                configuration = (IDependencyConfiguration)configInstance;
            }
            return configuration;
        }
    }
}
