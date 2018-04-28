using Indigo.Functions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ConfigurationFunctionSample
{
    public static class ConfigFunction
    {
        [FunctionName("ConfigFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequest req,
            [Config(SettingName = "test1")] string settingValue1,
            [Config(SettingName = "test2")] string settingValue2,
            [Config("test3")] string settingValue3,
            TraceWriter log)
        {
            return new OkObjectResult(
                $"Values are test1: {settingValue1}, test2: {settingValue2}, test3: {settingValue3}");
        }
    }
}
