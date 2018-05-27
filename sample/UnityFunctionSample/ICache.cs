using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace UnityFunctionSample
{
    public interface ICache
    {
        string StringGet(string key);

        void StringSet(string key, string value);
    }

    public class CacheProvider : ICache
    {
        private readonly ICacheConfigProvider _configProvider;
        private readonly ILogger _logger;
        private readonly LinkedList<string> _orderedKeys; // oldest key elimination policy
        private readonly IDictionary<string, string> _values;

        public CacheProvider(ICacheConfigProvider configProvider, ILogger logger)
        {
            _configProvider = configProvider;
            _logger = logger;
            _orderedKeys = new LinkedList<string>();
            _values = new Dictionary<string, string>();
        }

        public string StringGet(string key)
        {
            _logger.LogInformation($"{typeof(CacheProvider)}: received query for key '{key}'");

            return _values.ContainsKey(key) ? _values[key] : null;
        }

        public void StringSet(string key, string value)
        {
            _logger.LogInformation($"{typeof(CacheProvider)}: storing value for key '{key}'");

            if (!_values.ContainsKey(key))
            {
                _orderedKeys.AddLast(key);
            }

            if (_orderedKeys.Count > _configProvider.GetCacheSize())
            {
                var keyToRemove = _orderedKeys.First.Value;
                _orderedKeys.RemoveFirst();
                _values.Remove(keyToRemove);
            }

            _values[key] = value;
        }
    }
}
