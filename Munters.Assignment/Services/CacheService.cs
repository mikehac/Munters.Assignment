using Microsoft.Extensions.Caching.Memory;
using Munters.Assignment.DTOs;

namespace Munters.Assignment.Services
{
    /// <summary>
    /// I desided to export cache implementation to another service
    /// in order to allow to substitute it with another implementation in the future
    /// if we want to
    /// </summary>
    public class CacheService : ICacheService
    {
        readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public GiphyResponseDTO? Get(string query)
        {
            var result = _memoryCache.Get<GiphyResponseDTO>(query);
            return result;
        }

        public void Set(string query, GiphyResponseDTO value)
        {
            // Here I limit the time for cache to be saved for 60 seconds
            // For production I would set it into apsetting configuration file
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(60));
            _memoryCache.Set<GiphyResponseDTO>(query, value, options);
        }
    }
}
