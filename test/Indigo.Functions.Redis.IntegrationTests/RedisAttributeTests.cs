using StackExchange.Redis;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Indigo.Functions.Redis.IntegrationTests
{
    public class RedisAttributeTests
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly Lazy<Task<IDatabase>> _database;

        public RedisAttributeTests()
        {
            _database = new Lazy<Task<IDatabase>>(async () =>
            {
                var connectionMultiplexer = await ConnectionMultiplexer
                    .ConnectAsync("localhost")
                    .ConfigureAwait(false);
                return connectionMultiplexer.GetDatabase();
            });
        }

        [Fact]
        public async Task Redis_GetStringValue_ValueReadFromRedis()
        {
            var key = "redis_key";
            var value = Path.GetRandomFileName();
            var database = await _database.Value;
            await database.StringSetAsync(key, value);

            var response =
                await httpClient.GetAsync($"http://localhost:7075/test/{key}");
            var actualValue = await response.Content.ReadAsStringAsync();
            
            Assert.Equal(value, actualValue);
        }

        [Fact]
        public async Task Redis_SetStringValue_ValueSetInRedis()
        {
            var key = "redis_key";
            var value = Path.GetRandomFileName();
            var response =
                await httpClient.PostAsync($"http://localhost:7075/test/{key}", new StringContent(value));

            var database = await _database.Value;
            var actualValue = await database.StringGetAsync(key);

            Assert.Equal(value, actualValue);
        }
    }
}
