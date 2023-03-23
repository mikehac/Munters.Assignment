using Munters.Assignment.DTOs;

namespace Munters.Assignment.Services
{
    public interface ICacheService
    {
        Task<GiphyResponseDTO> GetOrCreate(string cacheKey, Func<Task<GiphyResponseDTO>> func);
    }
}
