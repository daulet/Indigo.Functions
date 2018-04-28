using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using System;

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
                .BindToInput(GetSettingValueFromAppConfig<dynamic>);
        }

        private T GetSettingValueFromAppConfig<T>(ConfigAttribute attribute)
        {
            return (T)Convert.ChangeType(_config[attribute.SettingName], typeof(T));
        }
    }
}
