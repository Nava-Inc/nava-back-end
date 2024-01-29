using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nava.Interface;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowAllOrigins")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly IMusicRepository _musicRepository;

    public SearchController(ILogger<SearchController> logger, IMusicRepository musicRepository)
    {
        _logger = logger;
        _musicRepository = musicRepository;
    }

    [HttpGet(Name = "SearchMusic")]
    public IActionResult SearchMusic([FromQuery] string query)
    {
        var searchResults = _musicRepository.SearchMusic(query);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(searchResults.IsNullOrEmpty() ? "No music found for the given query" : searchResults);
    }
}