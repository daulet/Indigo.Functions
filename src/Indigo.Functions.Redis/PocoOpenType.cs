using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Bindings;
using StackExchange.Redis;
using System;

namespace Indigo.Functions.Redis
{
    public class PocoOpenType : OpenType
    {
        public override bool IsMatch(Type type, OpenTypeMatchContext context)
        {
            return type != typeof(IConnectionMultiplexer)
                && type != typeof(IDatabase)
                && type != typeof(string)
                && (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IAsyncCollector<>));
        }
    }
}
