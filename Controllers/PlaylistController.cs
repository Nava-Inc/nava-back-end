using Microsoft.AspNetCore.Mvc;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaylistController : ControllerBase
{
    private readonly ILogger<PlaylistController> _logger;

    public PlaylistController(ILogger<PlaylistController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "PlaylistMusics")]
    public async Task<IActionResult> Musics()
    {
        throw new NotImplementedException();
    }

    [HttpPost(Name = "AddMusicToPlaylist")]
    public async Task<IActionResult> Add()
    {
        throw new NotImplementedException();
    }

    [HttpDelete(Name = "RemoveMusicFromPlaylist")]
    public async Task<IActionResult> Remove()
    {
        throw new NotImplementedException();
    }
}