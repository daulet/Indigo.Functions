using Indigo.Functions.Injection.Internal;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Indigo.Functions.Injection
{
    public class InjectExtension : IExtensionConfigProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IDependencyConfiguration _dependencyConfiguration;
        private readonly ILoggerFactory _loggerFactory;

        public InjectExtension(
            IConfiguration configuration,
            IDependencyConfiguration dependencyConfiguration,
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _dependencyConfiguration = dependencyConfiguration;
            _loggerFactory = loggerFactory;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<InjectAttribute>();
            rule.BindToInput<Anonymous>((attribute) => null);

            var serviceCollection = new ServiceCollection();
            _dependencyConfiguration.RegisterServices(serviceCollection);

            serviceCollection.AddSingleton(_configuration);

            var logger = _loggerFactory.CreateLogger("Host.General");
            serviceCollection.AddSingleton(logger);

            var container = serviceCollection.BuildServiceProvider();
            rule.AddOpenConverter<Anonymous, OpenType>(typeof(InjectConverter<>), container);
        }
    }
}
