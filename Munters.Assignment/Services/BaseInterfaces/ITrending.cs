using Munters.Assignment.DTOs;

namespace Munters.Assignment.Services.BaseInterfaces
{
    public interface ITrending
    {
        Task<GiphyResponseDTO> GetTrending();
    }
}
