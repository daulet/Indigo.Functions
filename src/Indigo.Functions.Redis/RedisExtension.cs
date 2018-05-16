using Microsoft.Azure.WebJobs.Host.Config;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Indigo.Functions.Redis
{
    public class RedisExtension : IExtensionConfigProvider
    {
        private readonly Dictionary<string, IConnectionMultiplexer> _connections;

        public RedisExtension()
        {
            _connections = new Dictionary<string, IConnectionMultiplexer>();
        }

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

        private IConnectionMultiplexer GetConnectionMultiplexerValueFromAttribute(RedisAttribute attribute)
        {
            if (!_connections.ContainsKey(attribute.Configuration))
            {
                _connections[attribute.Configuration] = ConnectionMultiplexer.Connect(attribute.Configuration);
            }
            return _connections[attribute.Configuration];
        }

        private IDatabase GetDatabaseValueFromAttribute(RedisAttribute attribute)
        {
            var connectionMultiplexer = GetConnectionMultiplexerValueFromAttribute(attribute);
            return connectionMultiplexer.GetDatabase();
        }

        private string GetStringValueFromAttribute(RedisAttribute attribute)
        {
            var connectionMultiplexer = GetConnectionMultiplexerValueFromAttribute(attribute);
            return connectionMultiplexer.GetDatabase().StringGet(attribute.Key);
        }
    }
}
