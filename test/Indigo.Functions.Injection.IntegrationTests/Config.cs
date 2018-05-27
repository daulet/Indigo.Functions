using Microsoft.Extensions.Configuration;

namespace Indigo.Functions.Injection.IntegrationTests
{
    internal class Config
    {
        private readonly IConfigurationRoot _config;

        public Config()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("test.json")
                .Build();
        }

        public string MisconfiguredTargetUrl => _config["TestMisconfiguredTargetUri"];

        public string TargetUrl => _config["TestTargetUri"];
    }
}
