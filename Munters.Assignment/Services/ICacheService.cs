using Munters.Assignment.DTOs;

namespace Munters.Assignment.Services
{
    public interface ICacheService
    {
        GiphyResponseDTO? Get(string query);
        void Set(string query, GiphyResponseDTO value);
    }
}
