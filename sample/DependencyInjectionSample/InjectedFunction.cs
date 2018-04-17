using Indigo.Functions.Injection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace DependencyInjectionSample
{
    public static class InjectedFunction
    {
        [FunctionName("DependencyInjection")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequest req,
            [Inject] IStorageAccess dependency,
            TraceWriter log)
        {
            return new OkObjectResult($"Instance of dependency {dependency.GetType()}");
        }
    }
}
