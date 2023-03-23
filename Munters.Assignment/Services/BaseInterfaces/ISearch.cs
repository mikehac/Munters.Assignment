using Munters.Assignment.DTOs;

namespace Munters.Assignment.Services.BaseInterfaces
{
    public interface ISearch
    {
        Task<GiphyResponseDTO> SearchAsync(string query);
    }
}
