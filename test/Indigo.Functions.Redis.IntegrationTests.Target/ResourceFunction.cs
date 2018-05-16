using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using StackExchange.Redis;

namespace Indigo.Functions.Redis.IntegrationTests.Target
{
    public static class ResourceFunction
    {
        [FunctionName("ResourceFunction1")]
        public static IActionResult GetMultiplexer1(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "resource/multiplexer1")] HttpRequest request,
            [Redis] IConnectionMultiplexer connectionMultiplexer)
        {
            return new OkObjectResult($"{connectionMultiplexer.GetHashCode()}");
        }

        [FunctionName("ResourceFunction2")]
        public static IActionResult GetMultiplexer2(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "resource/multiplexer2")] HttpRequest request,
            [Redis] IConnectionMultiplexer connectionMultiplexer)
        {
            return new OkObjectResult($"{connectionMultiplexer.GetHashCode()}");
        }
    }
}
