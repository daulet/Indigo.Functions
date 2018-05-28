using Microsoft.Extensions.Configuration;

namespace Sample.Storage
{
    public interface ICacheConfigProvider
    {
        int GetCacheSize();
    }

    public class CacheConfigProvider : ICacheConfigProvider
    {
        private readonly IConfiguration _configuration;

        public CacheConfigProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int GetCacheSize()
        {
            return int.Parse(_configuration["cache_size"]);
        }
    }
}
