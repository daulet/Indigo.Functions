using Autofac;
using Indigo.Functions.Autofac.Internal;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using System;
using System.Linq;

namespace Indigo.Functions.Autofac
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
                var containerBuilder = new ContainerBuilder();
                dependencyConfig.RegisterComponents(containerBuilder);
                containerBuilder.RegisterInstance(context.Config.LoggerFactory.CreateLogger("Host.General"));

                var container = containerBuilder.Build();
                rule.AddOpenConverter<Anonymous, OpenType>(typeof(InjectConverter<>), context.Config, container);
            }
        }

        private static IDependencyConfig InitializeContainer(ExtensionConfigContext context)
        {
            var configType = context.Config.TypeLocator.GetTypes()
                .Where(x => typeof(IDependencyConfig).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .FirstOrDefault();

            IDependencyConfig dependencyConfig = null;
            if (configType != null)
            {
                var configInstance = Activator.CreateInstance(configType);
                dependencyConfig = (IDependencyConfig)configInstance;
            }
            return dependencyConfig;
        }
    }
}
