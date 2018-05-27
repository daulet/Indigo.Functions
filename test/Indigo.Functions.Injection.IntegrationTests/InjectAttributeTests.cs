using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Indigo.Functions.Injection.IntegrationTests
{
    public class InjectAttributeTests
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly Config config = new Config();

        [Fact]
        public async Task Inject_ConfigExists_InstanceInjected()
        {
            var response =
                await httpClient.GetAsync($"{config.TargetUrl}/Dependency");

            Assert.True(response.IsSuccessStatusCode, "Failed to send HTTP GET");
        }

        [Theory]
        [InlineData("setting1", "value1")]
        [InlineData("setting2", "value2")]
        public async Task Inject_DependencyOnIConfiguration_SettingRead(string settingName, string expectedValue)
        {
            var response = await httpClient.GetAsync($"{config.TargetUrl}/config/{settingName}");
            var value = await response.Content.ReadAsStringAsync();

            Assert.Equal(expectedValue, value);
        }

        [Fact]
        public async Task Inject_DependencyOnILogger_ILoggerInjected()
        {
            var response =
                await httpClient.GetAsync($"{config.TargetUrl}/LoggingDependency");

            Assert.True(response.IsSuccessStatusCode, "Failed to send HTTP GET");
        }

        [Fact]
        public async Task Inject_NonPublicConfig_FunctionFailsToResolveDependency()
        {
            var response =
                await httpClient.GetAsync($"{config.MisconfiguredTargetUrl}/NonPublicConfigFunction");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
