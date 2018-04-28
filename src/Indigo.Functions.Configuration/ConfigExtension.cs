using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;

namespace Indigo.Functions.Configuration
{
    public class ConfigExtension : IExtensionConfigProvider
    {
        private IConfigurationRoot _config;

        public void Initialize(ExtensionConfigContext context)
        {
            _config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var rule = context.AddBindingRule<ConfigAttribute>();
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig);
        }

        private string GetSettingValueFromAppConfig(ConfigAttribute attribute)
        {
            return _config[attribute.SettingName];
        }
    }
}
