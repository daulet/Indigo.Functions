using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Indigo.Functions.Redis.IntegrationTests.Target
{
    public static class PocoFunction
    {
        [FunctionName("GetPoco")]
        public static IActionResult GetPoco(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "poco/{path}")] HttpRequest request,
            [Redis(Key = "{path}")] CustomObject cachedValue,
            TraceWriter log)
        {
            return new OkObjectResult(JsonConvert.SerializeObject(cachedValue));
        }

        [FunctionName("SetPoco")]
        public static async Task<IActionResult> SetPoco(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poco/{path}")] HttpRequest request,
            [Redis(Key = "{path}")] IAsyncCollector<CustomObject> collector,
            TraceWriter log)
        {
            string requestBody;
            using (var reader = new StreamReader(request.Body))
            {
                requestBody = reader.ReadToEnd();
                var value = JsonConvert.DeserializeObject<CustomObject>(requestBody);
                await collector.AddAsync(value);
            }
            return new OkObjectResult(requestBody);
        }
    }
}
