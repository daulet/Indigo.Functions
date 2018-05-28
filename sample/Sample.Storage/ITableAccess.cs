using Microsoft.Extensions.Logging;

namespace Sample.Storage
{
    public interface ITableAccess
    {
        string QueryByKey(string key);
    }

    public class CloudTableAccess : ITableAccess
    {
        private readonly ILogger _logger;

        public CloudTableAccess(ILogger logger)
        {
            _logger = logger;
        }

        public string QueryByKey(string key)
        {
            _logger.LogInformation($"{typeof(CloudTableAccess)}: received query for key '{key}'");

            // simplified as this is a sample project
            return $"Value stored at {key}";
        }
    }
}
