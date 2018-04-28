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

        [Theory]
        [InlineData("http://localhost:7072/test/bool")]
        [InlineData("http://localhost:7072/test/byte")]
        [InlineData("http://localhost:7072/test/sbyte")]
        [InlineData("http://localhost:7072/test/char")]
        [InlineData("http://localhost:7072/test/datetime")]
        [InlineData("http://localhost:7072/test/datetimeoffset")]
        [InlineData("http://localhost:7072/test/decimal")]
        [InlineData("http://localhost:7072/test/double")]
        [InlineData("http://localhost:7072/test/float")]
        [InlineData("http://localhost:7072/test/guid")]
        [InlineData("http://localhost:7072/test/int")]
        [InlineData("http://localhost:7072/test/uint")]
        [InlineData("http://localhost:7072/test/long")]
        [InlineData("http://localhost:7072/test/ulong")]
        [InlineData("http://localhost:7072/test/short")]
        [InlineData("http://localhost:7072/test/ushort")]
        [InlineData("http://localhost:7072/test/string")]
        [InlineData("http://localhost:7072/test/timespan")]
        public async Task Config_AllBuiltInTypes_ValueRead(string url)
        {
            var response =
                await httpClient.GetAsync(url);

            Assert.True(response.IsSuccessStatusCode, "See the function implementation for expectation");
        }
    }
}
