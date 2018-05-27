using Microsoft.Extensions.Logging;

namespace AutofacFunctionSample
{
    public interface IStorageAccess
    {
        string RetrieveValue(string key);
    }

    public class StorageAccess : IStorageAccess
    {
        private readonly ICache _cache;
        private readonly ILogger _logger;
        private readonly ITableAccess _tableAccess;

        public StorageAccess(ICache cache, ILogger logger, ITableAccess tableAccess)
        {
            _cache = cache;
            _logger = logger;
            _tableAccess = tableAccess;
        }

        public string RetrieveValue(string key)
        {
            var value = _cache.StringGet(key);

            if (value == null)
            {
                _logger.LogInformation($"{typeof(StorageAccess)}: cache miss for '{key}', querying table");

                value = _tableAccess.QueryByKey(key);
                _cache.StringSet(key, value);
            }

            return value;
        }
    }
}
