using Munters.Assignment.Services.BaseInterfaces;

namespace Munters.Assignment.Services
{
    /// <summary>
    /// Here I imlemented Interface segregation principle of SOLID
    /// So if in the future we have a requirement to imlement only ISearch(for example)
    /// We wont be forced to imlement another unnecessary functionality
    /// </summary>
    public interface IGiphyService : ISearch, ITrending
    {
    }
}
