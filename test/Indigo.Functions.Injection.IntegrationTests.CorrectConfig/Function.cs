using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Indigo.Functions.Injection.IntegrationTests.CorrectConfig
{
    public static class Function
    {
        [FunctionName("CorrectFunction")]
        public static IActionResult CorrectFunction(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = null)] HttpRequest req,
            [Inject] IDependency dependency,
            TraceWriter log)
        {
            return new OkObjectResult($"Instance of dependency {dependency.GetType()}");
        }
    }
}
