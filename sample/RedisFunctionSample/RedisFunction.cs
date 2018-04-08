using Indigo.Functions.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using StackExchange.Redis;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RedisFunctionSample
{
    public static class RedisFunction
    {
        [FunctionName("Redis_GetKey")]
        public static string GetKey(
            [HttpTrigger("GET", Route = "cache/{key}")] HttpRequest req,
            string key,
            [Redis] IConnectionMultiplexer connectionMultiplexer)
        {
            var database = connectionMultiplexer.GetDatabase();
            return database.StringGet(key);
        }

        [FunctionName("Redis_ListKeys")]
        public static IEnumerable<string> ListKeys(
            [HttpTrigger("GET", Route = "cache")] HttpRequest req,
            [Redis] IConnectionMultiplexer connectionMultiplexer)
        {
            string pattern = req.Query["pattern"];

            var randomEndpoint = connectionMultiplexer.GetEndPoints().First();
            var server = connectionMultiplexer.GetServer(randomEndpoint);
            var keys = server.Keys(pattern: pattern);

            return keys.Select(x => x.ToString());
        }

        [FunctionName("Redis_SetKey")]
        public static IActionResult SetKey(
            [HttpTrigger("POST", Route = "cache/{key}")] HttpRequest req,
            string key,
            [Redis] IConnectionMultiplexer connectionMultiplexer)
        {
            string value = null;
            using (var reader = new StreamReader(req.Body))
            {
                value = reader.ReadToEnd();
            }
            if (string.IsNullOrEmpty(value))
            {
                return new OkObjectResult("No value specified");
            }

            var database = connectionMultiplexer.GetDatabase();
            database.StringSet(key, value);
            return new OkObjectResult($"{key} = {value}");
        }
    }
}
