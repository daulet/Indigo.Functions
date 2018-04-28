using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;

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
               .BindToInput(GetDateTimeFromAppConfig);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
               .BindToInput(GetDateTimeOffsetFromAppConfig);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<decimal>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<double>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
                .BindToInput(GetSettingValueFromAppConfig<float>);
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
               .BindToInput(GetGuidFromAppConfig);
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
            rule.WhenIsNotNull(nameof(ConfigAttribute.SettingName))
               .BindToInput(GetTimeSpanFromAppConfig);
        }

        private DateTime GetDateTimeFromAppConfig(ConfigAttribute attribute)
        {
            return DateTime.Parse(_config[attribute.SettingName], CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }

        private DateTimeOffset GetDateTimeOffsetFromAppConfig(ConfigAttribute attribute)
        {
            return DateTimeOffset.Parse(_config[attribute.SettingName], CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }

        private Guid GetGuidFromAppConfig(ConfigAttribute attribute)
        {
            return Guid.Parse(_config[attribute.SettingName]);
        }

        private T GetSettingValueFromAppConfig<T>(ConfigAttribute attribute)
        {
            return (T)Convert.ChangeType(_config[attribute.SettingName], typeof(T));
        }

        private TimeSpan GetTimeSpanFromAppConfig(ConfigAttribute attribute)
        {
            return TimeSpan.Parse(_config[attribute.SettingName]);
        }
    }
}
