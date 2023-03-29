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

        /// <summary>
        /// This method is build for multiple concurent requests.
        /// We lock the access to cache object, so only single request 
        /// can get data from it simultaneously
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<GiphyResponseDTO> GetOrCreate(string cacheKey,Func<Task<GiphyResponseDTO>> func)
        {
            var semaphore = new SemaphoreSlim(1, 1);

            var result = _memoryCache.Get<GiphyResponseDTO>(cacheKey);
            if (result is not null)
            {
                _logger.LogInformation($"Cache was found for query {cacheKey}");
                return result;
            }
            try
            {
                await semaphore.WaitAsync();
                result = _memoryCache.Get<GiphyResponseDTO>(cacheKey);
                if (result is not null)
                {
                    _logger.LogInformation($"Cache was found for query {cacheKey} after recheck");
                    return result;
                }

                result = await func();

                if (result.responseStatusCode == 200)
                {
                    _logger.LogInformation($"The query {cacheKey} was added to the cache");

                    // Here I limit the time for cache to be saved for 60 seconds
                    // For production I would set it into apsetting configuration file
                    MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                    options.SetSlidingExpiration(TimeSpan.FromSeconds(60));
                    _memoryCache.Set<GiphyResponseDTO>(cacheKey, result, options);
                }
            }
            finally
            {
                semaphore.Release();
            }

            return result;
        }
    }
}
