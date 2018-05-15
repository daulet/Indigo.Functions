using Indigo.Functions.Unity.Internal;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Unity;

namespace Indigo.Functions.Unity
{
    public class InjectExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<InjectAttribute>();

            rule.BindToInput<Anonymous>((attribute) => null);

            var container = InitializeContainer(context)?.Container;
            if (container != null)
            {
                container.RegisterInstance(context.Config.LoggerFactory.CreateLogger("Host.General"));
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
