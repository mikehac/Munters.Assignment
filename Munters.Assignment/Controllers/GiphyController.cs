using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Munters.Assignment.Services;

namespace Munters.Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiphyController : ControllerBase
    {
        private readonly IGiphyService _giphyService;
        private readonly ILogger<GiphyController> _logger;

        public GiphyController(IGiphyService giphyService, ILogger<GiphyController> logger)
        {
            _giphyService = giphyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _giphyService.GetTrending();
            if (result == null || !result.data.Any())
            {
                _logger.LogInformation($"No giphies were found");
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> Get(string query)
        {
            var result = await _giphyService.SearchAsync(query);
            if (result == null || !result.data.Any())
            {
                _logger.LogInformation($"No giphies were found for query {query}");
                return NotFound();
            }

            return Ok(result);
        }
    }
}
