using Indigo.Functions.Unity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Sample.Storage;

namespace UnityFunctionSample
{
    public static class InjectedFunction
    {
        [FunctionName("InjectableFunctionExample")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "{key}")] HttpRequest req,
            string key,
            [Inject] IStorageAccess storageAccess,
            ILogger logger)
        {
            logger.LogInformation($"Injected instance of {typeof(IStorageAccess)} is {storageAccess.GetType()}");

            var value = storageAccess.RetrieveValue(key);
            return new OkObjectResult($"Value for key '{key}' = '{value}'");
        }
    }
}
