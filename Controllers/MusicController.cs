using Microsoft.AspNetCore.Mvc;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
public class MusicController : ControllerBase
{
    private readonly ILogger<MusicController> _logger;

    public MusicController(ILogger<MusicController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "MusicInfo")]
    public IActionResult MusicInfo()
    {
        // return info of a music, including the user interactions with that music
        throw new NotImplementedException();
    }
    
    
    [HttpGet("download", Name = "Download")]
    public IActionResult Download()
    {
        // return music file
        throw new NotImplementedException();
    }
    
    [HttpPatch("like", Name = "ToggleLike")]
    public IActionResult ToggleLike([FromBody] bool likeStatus)
    {
        throw new NotImplementedException();
    }
    
    
    [HttpPost(Name = "Comment")]
    public IActionResult Comment([FromBody] string comment)
    {
        throw new NotImplementedException();
    }
}