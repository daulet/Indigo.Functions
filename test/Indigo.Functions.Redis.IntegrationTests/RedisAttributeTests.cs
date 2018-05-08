using Indigo.Functions.Redis.IntegrationTests.Target;
using Newtonsoft.Json;
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
        public async Task Redis_GetMultiplexer_ValueReadFromRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var value = Path.GetRandomFileName();
            var database = await _database.Value;
            await database.StringSetAsync(key, value);

            // Act
            var response = await httpClient.GetAsync($"http://localhost:7075/test/multiplexer/{key}");

            // Assert
            var actualValue = await response.Content.ReadAsStringAsync();
            Assert.Equal(value, actualValue);
        }

        [Fact]
        public async Task Redis_GetMultiplexerAsync_ValueReadFromRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var value = Path.GetRandomFileName();
            var database = await _database.Value;
            await database.StringSetAsync(key, value);

            // Act
            var response = await httpClient.GetAsync($"http://localhost:7075/test/multiplexerasync/{key}");

            // Assert
            var actualValue = await response.Content.ReadAsStringAsync();
            Assert.Equal(value, actualValue);
        }

        [Fact]
        public async Task Redis_GetDatabase_ValueReadFromRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var value = Path.GetRandomFileName();
            var database = await _database.Value;
            await database.StringSetAsync(key, value);

            // Act
            var response = await httpClient.GetAsync($"http://localhost:7075/test/database/{key}");

            // Assert
            var actualValue = await response.Content.ReadAsStringAsync();
            Assert.Equal(value, actualValue);
        }

        [Fact]
        public async Task Redis_GetDatabaseAsync_ValueReadFromRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var value = Path.GetRandomFileName();
            var database = await _database.Value;
            await database.StringSetAsync(key, value);

            // Act
            var response = await httpClient.GetAsync($"http://localhost:7075/test/databaseasync/{key}");

            // Assert
            var actualValue = await response.Content.ReadAsStringAsync();
            Assert.Equal(value, actualValue);
        }

        [Fact]
        public async Task Redis_GetStringValue_ValueReadFromRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var value = Path.GetRandomFileName();
            var database = await _database.Value;
            await database.StringSetAsync(key, value);

            // Act
            var response = await httpClient.GetAsync($"http://localhost:7075/test/string/{key}");

            // Assert
            var actualValue = await response.Content.ReadAsStringAsync();
            Assert.Equal(value, actualValue);
        }

        [Fact]
        public async Task Redis_SetStringValue_ValueSetInRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var value = Path.GetRandomFileName();

            // Act
            var response =
                await httpClient.PostAsync($"http://localhost:7075/test/string/{key}", new StringContent(value));

            // Assert
            var database = await _database.Value;
            var actualValue = await database.StringGetAsync(key);
            Assert.Equal(value, actualValue);
        }

        [Fact]
        public async Task Redis_GetPocoValue_ValueReadFromRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var expectedObject = new CustomObject()
            {
                IntegerProperty = new Random().Next(),
                StringProperty = Path.GetRandomFileName(),
            };
            var database = await _database.Value;
            await database.StringSetAsync(key, JsonConvert.SerializeObject(expectedObject));

            // Act
            var response = await httpClient.GetAsync($"http://localhost:7075/test/poco/{key}");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            var actualObject = JsonConvert.DeserializeObject<CustomObject>(content);
            Assert.Equal(expectedObject.IntegerProperty, actualObject.IntegerProperty);
            Assert.Equal(expectedObject.StringProperty, actualObject.StringProperty);
        }

        [Fact]
        public async Task Redis_InvalidPocoStored_NullFetched()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var database = await _database.Value;
            await database.StringSetAsync(key, Path.GetRandomFileName());

            // Act
            var response = await httpClient.GetAsync($"http://localhost:7075/test/poco/{key}");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            var actualObject = JsonConvert.DeserializeObject<CustomObject>(content);
            Assert.Null(actualObject);
        }

        [Fact]
        public async Task Redis_SetPocoValue_ValueReadFromRedis()
        {
            // Arrange
            var key = Path.GetRandomFileName();
            var expectedObject = new CustomObject()
            {
                IntegerProperty = new Random().Next(),
                StringProperty = Path.GetRandomFileName(),
            };

            // Act
            var response = await httpClient.PostAsync($"http://localhost:7075/test/poco/{key}",
                new StringContent(JsonConvert.SerializeObject(expectedObject)));
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            var database = await _database.Value;
            var actualValue = await database.StringGetAsync(key);
            var actualObject = JsonConvert.DeserializeObject<CustomObject>(content);
            Assert.Equal(expectedObject.IntegerProperty, actualObject.IntegerProperty);
            Assert.Equal(expectedObject.StringProperty, actualObject.StringProperty);
        }
    }
}
