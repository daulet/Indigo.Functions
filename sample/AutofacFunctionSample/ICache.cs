using Microsoft.Extensions.Logging;

namespace AutofacFunctionSample
{
    public interface ICache
    {
        string StringGet(string key);
    }

    public class CacheProvider : ICache
    {
        private readonly ILogger _logger;

        public CacheProvider(ILogger logger)
        {
            this._logger = logger;
        }

        public string StringGet(string key)
        {
            _logger.LogInformation($"{typeof(CacheProvider)}: received query for key '{key}'");

            // simplified as this is a sample project
            return null;
        }
    }
}
