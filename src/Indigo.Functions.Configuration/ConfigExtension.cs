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
                .BindToInput(GetSettingValueFromAppConfig<bool>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<byte>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<sbyte>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<char>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<decimal>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<double>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<float>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<int>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<uint>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<long>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<ulong>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<short>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<ushort>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
               .BindToInput(GetSettingValueFromAppConfig<string>);
        }

        private T GetSettingValueFromAppConfig<T>(ConfigAttribute attribute)
        {
            return (T)Convert.ChangeType(_config[attribute.SettingName], typeof(T));
        }
    }
}
