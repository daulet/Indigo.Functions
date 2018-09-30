using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sample.Storage;

[assembly: WebJobsStartup(typeof(InjectionFunctionSample.Startup))]
namespace InjectionFunctionSample
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<ICache, CacheProvider>();
            builder.Services.AddTransient<ICacheConfigProvider, CacheConfigProvider>();
            builder.Services.AddTransient<IStorageAccess, StorageAccess>();
            builder.Services.AddTransient<ITableAccess, CloudTableAccess>();
        }
    }
}
