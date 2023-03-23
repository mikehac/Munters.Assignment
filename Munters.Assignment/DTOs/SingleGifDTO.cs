namespace Munters.Assignment.DTOs
{
    /// <summary>
    /// This class is for transfering the data to the end user
    /// I desided to export Title and Image url as well
    /// so the end user(Angular application) can use it for ui
    /// </summary>
    public class SingleGifDTO
    {
        public string SiteUrl { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }        
    }
}
