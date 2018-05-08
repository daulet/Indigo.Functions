using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Indigo.Functions.Redis.IntegrationTests.Target
{
    public static class DatabaseFunction
    {
        [FunctionName("DatabaseFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "database/{key}")] HttpRequest request,
            string key,
            [Redis] IDatabase database,
            TraceWriter log)
        {
            string value = database.StringGet(key);
            return new OkObjectResult(value);
        }

        [FunctionName("DatabaseAsyncFunction")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "databaseasync/{key}")] HttpRequest request,
            string key,
            [Redis] IDatabase database,
            TraceWriter log)
        {
            string value = await database.StringGetAsync(key);
            return new OkObjectResult(value);
        }
    }
}
