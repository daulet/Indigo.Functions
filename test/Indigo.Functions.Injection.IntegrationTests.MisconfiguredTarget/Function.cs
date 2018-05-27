using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Indigo.Functions.Injection.IntegrationTests.MisconfiguredTarget
{
    public static class Function1
    {
        [FunctionName("NonPublicConfigFunction")]
        public static IActionResult NonPublicConfigFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "NonPublicConfigFunction")] HttpRequest req,
            [Inject] IDependency dependency,
            TraceWriter log)
        {
            return new OkObjectResult($"Instance of dependency {dependency.GetType()}");
        }
    }
}
