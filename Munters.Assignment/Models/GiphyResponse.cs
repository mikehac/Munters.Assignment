namespace Munters.Assignment.Models
{
    /// <summary>
    /// This class represents entire response from Giphy api
    /// </summary>
    public class GiphyResponse
    {
        public SingleGif[] data { get; set; }
    }
}
