using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Indigo.Functions.Injection.IntegrationTests.CorrectConfig
{
    public static class Function
    {
        [FunctionName("DependencyFunction")]
        public static IActionResult DependencyFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Dependency")] HttpRequest req,
            [Inject] IDependency dependency,
            ILogger log)
        {
            return new OkObjectResult($"Instance of dependency {dependency.GetType()}");
        }
    }
}
