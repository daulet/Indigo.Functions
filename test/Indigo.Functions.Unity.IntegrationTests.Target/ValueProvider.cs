using Microsoft.Extensions.Configuration;

namespace Indigo.Functions.Unity.IntegrationTests.Target
{
    public class ValueProvider
    {
        private readonly IConfiguration _configuration;

        public ValueProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSettingValue(string settingName)
        {
            return _configuration[settingName];
        }
    }
}
