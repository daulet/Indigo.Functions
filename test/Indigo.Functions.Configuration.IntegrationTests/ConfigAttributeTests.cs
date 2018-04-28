using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Indigo.Functions.Configuration.IntegrationTests
{
    public class ConfigAttributeTests
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [Fact]
        public async Task Config_NoSettingName_FunctionNotRegistered()
        {
            var response =
                await httpClient.GetAsync(@"http://localhost:7072/test/NoSettingName");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Config_StringType_ValueRead()
        {
            var response =
                await httpClient.GetAsync(@"http://localhost:7072/test/string");

            Assert.True(response.IsSuccessStatusCode, "See the function implementation for expectation");
        }
    }
}
