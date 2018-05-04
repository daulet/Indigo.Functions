using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using StackExchange.Redis;

namespace Indigo.Functions.Redis
{
    internal class RedisAsyncCollector : IAsyncCollector<string>
    {
        private readonly string _key;
        private readonly Lazy<Task<IDatabase>> _lazyDatabase;

        public RedisAsyncCollector(RedisAttribute attribute)
        {
            _key = attribute.Key;
            _lazyDatabase = new Lazy<Task<IDatabase>>(async () =>
            {
                var connectionMultiplexer = await ConnectionMultiplexer
                    .ConnectAsync(attribute.Configuration)
                    .ConfigureAwait(false);
                return connectionMultiplexer.GetDatabase();
            });
        }

        public async Task AddAsync(string item, CancellationToken cancellationToken = default(CancellationToken))
        {
            var database = await _lazyDatabase.Value.ConfigureAwait(false);
            await database.StringSetAsync(_key, item).ConfigureAwait(false);
        }

        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }
    }
}
