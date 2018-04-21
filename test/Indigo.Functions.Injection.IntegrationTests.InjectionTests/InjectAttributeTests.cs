using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Indigo.Functions.Injection.IntegrationTests.InjectionTests
{
    public class InjectAttributeTests
    {
        [Fact]
        public async Task Inject_ConfigExists_InstanceInjected()
        {
            var client = new HttpClient();
            var response =
                await client.GetAsync(@"http://localhost:7072/test/ConfiguredFunction");

            Assert.True(response.IsSuccessStatusCode, "Failed to send HTTP GET");
        }

        [Fact]
        public async Task Inject_NonPublicConfig_FunctionFailsToResolveDependency()
        {
            var client = new HttpClient();
            var response =
                await client.GetAsync(@"http://localhost:7073/test/NonPublicConfigFunction");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
