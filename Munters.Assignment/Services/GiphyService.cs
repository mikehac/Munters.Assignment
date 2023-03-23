﻿using AutoMapper;
using Microsoft.Extensions.Options;
using Munters.Assignment.DTOs;
using Munters.Assignment.Models;

namespace Munters.Assignment.Services
{
    /// <summary>
    /// Here Single Responsibility is imlemented.
    /// The class is responsible for retriving images from Giphy api
    /// </summary>
    public class GiphyService : IGiphyService
    {
        private readonly HttpClient _client;
        private readonly IOptions<AppSettings> _config;
        private readonly ICacheService _cache;
        private readonly ILogger<GiphyService> _logger;
        private readonly IMapper _mapper;

        private readonly string _trendingUrl;
        private readonly string _searchUrl;

        public GiphyService(HttpClient client, IOptions<AppSettings> config,
                            ICacheService cache, ILogger<GiphyService> logger,IMapper mapper)
        {
            _client = client;
            _config = config;
            _trendingUrl = string.Format(_config.Value.BaseGiphyUrl, "trending", _config.Value.ApiKey);
            _searchUrl = string.Format(_config.Value.BaseGiphyUrl, "search", _config.Value.ApiKey);
            _cache = cache;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GiphyResponseDTO> GetTrending()
        {
            return _mapper.Map<GiphyResponseDTO>(await _client.GetFromJsonAsync<GiphyResponse>(_trendingUrl));
        }

        public async Task<GiphyResponseDTO> SearchAsync(string query)
        {
            var cacheResult = _cache.Get(query);
            if (cacheResult is not null)
            {
                _logger.LogInformation($"Cache was found for query {query}");
                return cacheResult;
            }

            string fullSearchUrl = $"{_searchUrl}&q={query}";
            var result = _mapper.Map<GiphyResponseDTO>(await _client.GetFromJsonAsync<GiphyResponse>(fullSearchUrl));
            if (result is not null && result.data.Any())
            {
                _logger.LogInformation($"The query {query} was added to the cache");
                _cache.Set(query, result);
            }

            return result;
        }
    }
}
