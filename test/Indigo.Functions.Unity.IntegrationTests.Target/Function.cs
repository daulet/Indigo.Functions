using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Indigo.Functions.Unity.IntegrationTests.Target
{
    public static class Function
    {
        [FunctionName("ConfigurationFunction")]
        public static IActionResult ConfigurationFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "config/{key}")] HttpRequest req,
            string key,
            [Inject] ValueProvider provider)
        {
            return new OkObjectResult(provider.GetSettingValue(key));
        }

        [FunctionName("DependencyFunction")]
        public static IActionResult DependencyFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Dependency")] HttpRequest req,
            [Inject] IDependency dependency,
            ILogger log)
        {
            return new OkObjectResult($"Instance of dependency {dependency.GetType()}");
        }

        [FunctionName("LoggingDependencyFunction")]
        public static IActionResult LoggingDependencyFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "LoggingDependency")] HttpRequest req,
            [Inject] ILoggingDependency dependency,
            ILogger log)
        {
            dependency.Log($"Logging message to injected logger");

            return new OkObjectResult($"Instance of dependency {dependency.GetType()}");
        }
    }
}
