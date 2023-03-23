namespace Munters.Assignment.Models
{
    /// <summary>
    /// This class represents single item returned by Giphy api
    /// </summary>
    public class SingleGif
    {
        public string url { get; set; }
        public string title { get; set; }
        public SingleImage images { get; set; }
    }
    public class SingleImage
    {
        public Downsized downsized { get; set; }
    }
    public class Downsized
    {
        public string url { get; set; }
    }
}
