using Indigo.Functions.Injection.Internal;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Indigo.Functions.Injection
{
    public class InjectExtension : IExtensionConfigProvider
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceCollection _serviceCollection;

        public InjectExtension(
            ILoggerFactory loggerFactory,
            IServiceCollection serviceCollection)
        {
            _loggerFactory = loggerFactory;
            _serviceCollection = serviceCollection;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<InjectAttribute>();
            rule.BindToInput<Anonymous>((attribute) => null);

            var logger = _loggerFactory.CreateLogger("Host.General");
            _serviceCollection.AddSingleton(logger);

            var container = _serviceCollection.BuildServiceProvider();
            rule.AddOpenConverter<Anonymous, OpenType>(typeof(InjectConverter<>), container);
        }
    }
}
