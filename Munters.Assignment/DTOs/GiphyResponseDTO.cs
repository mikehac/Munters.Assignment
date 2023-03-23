namespace Munters.Assignment.DTOs
{
    public class GiphyResponseDTO
    {
        public int responseStatusCode { get; set; }
        public SingleGifDTO[] data { get; set; }
        public GiphyResponseDTO()
        {
            
        }
        public GiphyResponseDTO(int ResponseStatusCode)
        {
            responseStatusCode = ResponseStatusCode;
        }
    }
}
