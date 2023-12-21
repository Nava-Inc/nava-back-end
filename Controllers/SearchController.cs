using Microsoft.AspNetCore.Mvc;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;

    public SearchController(ILogger<SearchController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "SearchMusic")]
    public IActionResult SearchMusic([FromQuery] string query)
    {
        /*var searchResults = _musicRepository.SearchMusic(query);

        if (searchResults == null || !searchResults.Any())
        {
            return NotFound("No music found for the given query");
        }

        return Ok(searchResults);*/
        throw new NotImplementedException();
    }
}