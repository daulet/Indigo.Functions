using Microsoft.Azure.WebJobs.Host.Config;
using StackExchange.Redis;
using System;

namespace Indigo.Functions.Redis
{
    public class RedisExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<RedisAttribute>();

            rule.AddValidator(ValidateRedisAttribute);

            // inputs
            rule.WhenIsNull(nameof(RedisAttribute.Key))
                .BindToInput(GetConnectionMultiplexerValueFromAttribute);
            rule.WhenIsNull(nameof(RedisAttribute.Key))
                .BindToInput(GetDatabaseValueFromAttribute);
            rule.WhenIsNotNull(nameof(RedisAttribute.Key))
                .BindToInput(GetStringValueFromAttribute);

            // string output
            rule.WhenIsNotNull(nameof(RedisAttribute.Key))
                .BindToCollector(attribute => new RedisAsyncCollector(attribute));

            // generic converters
            rule.AddOpenConverter<PocoOpenType, string>(typeof(StringConverter<>));
            rule.AddOpenConverter<string, PocoOpenType>(typeof(PocoConverter<>));
        }

        private static void ValidateRedisAttribute(RedisAttribute attribute, Type parameterType)
        {
            if (string.IsNullOrEmpty(attribute.Configuration))
            {
                throw new ArgumentException("RedisAttribute.Configuration parameter cannot be null", nameof(attribute));
            }
        }

        private static IConnectionMultiplexer GetConnectionMultiplexerValueFromAttribute(RedisAttribute attribute)
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect(attribute.Configuration);
            return connectionMultiplexer;
        }

        private static IDatabase GetDatabaseValueFromAttribute(RedisAttribute attribute)
        {
            var connectionMultiplexer = GetConnectionMultiplexerValueFromAttribute(attribute);
            return connectionMultiplexer.GetDatabase();
        }

        private static string GetStringValueFromAttribute(RedisAttribute attribute)
        {
            var connectionMultiplexer = GetConnectionMultiplexerValueFromAttribute(attribute);
            return connectionMultiplexer.GetDatabase().StringGet(attribute.Key);
        }
    }
}
