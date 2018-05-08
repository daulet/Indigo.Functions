using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Indigo.Functions.Redis.IntegrationTests.Target
{
    public static class MultiplexerFunction
    {
        [FunctionName("MultiplexerFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "multiplexer/{key}")] HttpRequest request,
            string key,
            [Redis] IConnectionMultiplexer connectionMultiplexer,
            TraceWriter log)
        {
            var database = connectionMultiplexer.GetDatabase();
            string value = database.StringGet(key);
            return new OkObjectResult(value);
        }

        [FunctionName("MultiplexerAsyncFunction")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "multiplexerasync/{key}")] HttpRequest request,
            string key,
            [Redis] IConnectionMultiplexer connectionMultiplexer,
            TraceWriter log)
        {
            var database = connectionMultiplexer.GetDatabase();
            string value = await database.StringGetAsync(key);
            return new OkObjectResult(value);
        }
    }
}
