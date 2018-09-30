using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Indigo.Functions.Injection.IntegrationTests.Target.Startup))]
namespace Indigo.Functions.Injection.IntegrationTests.Target
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddTransient<IDependency, DependencyImpl>();
            builder.Services.AddTransient<ILoggingDependency, LoggingDependencyImpl>();
            builder.Services.AddTransient<ValueProvider>();
        }
    }
}
