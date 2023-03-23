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
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<GiphyResponseDTO> GetOrCreate(string cacheKey,Func<Task<GiphyResponseDTO>> func)
        {
            var result = _memoryCache.Get<GiphyResponseDTO>(cacheKey);
            if (result is not null)
            {
                _logger.LogInformation($"Cache was found for query {cacheKey}");
                return result;
            }

            result = await func();
            
            _logger.LogInformation($"The query {cacheKey} was added to the cache");

            // Here I limit the time for cache to be saved for 60 seconds
            // For production I would set it into apsetting configuration file
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(60));
            _memoryCache.Set<GiphyResponseDTO>(cacheKey, result, options);
            return result;
        }
    }
}
