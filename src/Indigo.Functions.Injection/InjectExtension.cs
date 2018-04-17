using Microsoft.Azure.WebJobs.Host.Config;
using System;
using System.Linq;
using System.Reflection;
using Unity;

namespace Indigo.Functions.Injection
{
    public class InjectExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<InjectAttribute>();

            var container = InitializeContainer(context)?.Container;
            if (container != null)
            {
                rule.Bind(new DependencyBindingProvider(container));
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
