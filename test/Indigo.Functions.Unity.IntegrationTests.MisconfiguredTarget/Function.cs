using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Indigo.Functions.Unity.IntegrationTests.MisconfiguredTarget
{
    public static class Function
    {
        [FunctionName("NonPublicConfigFunction")]
        public static IActionResult NonPublicConfigFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = null)] HttpRequest req,
            [Inject] IDependency dependency,
            TraceWriter log)
        {
            return new OkObjectResult($"Instance of dependency {dependency.GetType()}");
        }
    }
}
