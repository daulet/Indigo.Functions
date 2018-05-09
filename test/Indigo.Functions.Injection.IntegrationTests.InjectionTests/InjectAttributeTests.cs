using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Indigo.Functions.Injection.IntegrationTests.InjectionTests
{
    public class InjectAttributeTests
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [Fact]
        public async Task Inject_ConfigExists_InstanceInjected()
        {
            var response =
                await httpClient.GetAsync(@"http://localhost:7073/test/Dependency");

            Assert.True(response.IsSuccessStatusCode, "Failed to send HTTP GET");
        }

        [Fact]
        public async Task Inject_DependencyOnILogger_ILoggerInjected()
        {
            var response =
                await httpClient.GetAsync(@"http://localhost:7073/test/LoggingDependency");

            Assert.True(response.IsSuccessStatusCode, "Failed to send HTTP GET");
        }

        [Fact]
        public async Task Inject_NonPublicConfig_FunctionFailsToResolveDependency()
        {
            var response =
                await httpClient.GetAsync(@"http://localhost:7074/test/NonPublicConfigFunction");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
