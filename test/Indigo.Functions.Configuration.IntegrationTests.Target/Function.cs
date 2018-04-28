using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;

namespace Indigo.Functions.Configuration.IntegrationTests.Target
{
    public static class Function
    {
        [FunctionName("NoSettingNameFunction")]
        public static IActionResult NoSettingNameFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "NoSettingName")] HttpRequest req,
            [Config] string value,
            TraceWriter log)
        {
            return new OkObjectResult($"Parsed {value}");
        }

        [FunctionName("StringFunction")]
        public static IActionResult StringFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "string")] HttpRequest req,
            [Config("string")] string value,
            TraceWriter log)
        {
            if (value != "abc")
            {
                throw new ArgumentException("Couldn't parse the value");
            }
            return new OkObjectResult($"Parsed {value}");
        }
    }
}
