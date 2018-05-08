using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Bindings;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Indigo.Functions.Redis
{
    public class PocoOpenType : OpenType
    {
        public override bool IsMatch(Type type, OpenTypeMatchContext context)
        {
            return (type != typeof(IConnectionMultiplexer) && type != typeof(Task<IConnectionMultiplexer>))
                && (type != typeof(string) && type != typeof(Task<string>))
                && (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IAsyncCollector<>));
        }
    }
}
