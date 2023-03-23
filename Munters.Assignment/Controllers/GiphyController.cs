using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Munters.Assignment.DTOs;
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
            return ValidateResult(result);
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> Get(string query)
        {
            var result = await _giphyService.SearchAsync(query);
            return ValidateResult(result, query);
        }

        private IActionResult ValidateResult(GiphyResponseDTO response, string query = "")
        {
            // if the response from giphy server failed, reflect the status code to the client
            if (!response.responseStatusCode.Equals(200))
            {
                return response.responseStatusCode switch
                {
                    401 => Unauthorized(),
                    404 => NotFound(),
                    _ => BadRequest()
                };
            }

            // if the response from giphy server succeeded, but no giphies where found
            if (response == null || !response.data.Any())
            {
                _logger.LogInformation(string.IsNullOrEmpty(query) ?
                                          $"No giphies were found" :
                                          $"No giphies were found for query {query}");
                return NotFound();
            }

            return Ok(response);
        }
    }
}
