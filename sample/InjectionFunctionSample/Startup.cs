using Indigo.Functions.Injection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(InjectionFunctionSample.Startup))]
namespace InjectionFunctionSample
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            // register extension
            builder.AddExtension<InjectExtension>();

            // register implementation required by the extension
            builder.Services.AddSingleton<IDependencyConfiguration, DependencyConfiguration>();
        }
    }
}
