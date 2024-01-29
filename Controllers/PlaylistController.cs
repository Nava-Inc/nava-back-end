using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nava.Interface;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowAllOrigins")]
[Authorize]
public class PlaylistController : ControllerBase
{
    private readonly ILogger<PlaylistController> _logger;
    private readonly IPlaylistRepository _playlistRepository;

    public PlaylistController(ILogger<PlaylistController> logger, IPlaylistRepository playlistRepository)
    {
        _logger = logger;
        _playlistRepository = playlistRepository;
    }

    [HttpGet(Name = "PlaylistMusics")]
    public async Task<IActionResult> Musics(int playlistId)
    {
        var result = _playlistRepository.GetMusicByPlaylist(playlistId);
        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(result);
    }

    [HttpPost(Name = "AddMusicToPlaylist")]
    public async Task<IActionResult> Add(int musicId, int playlistId)
    {
        var result = _playlistRepository.AddMusicToPlaylist(playlistId, musicId);
        if (!ModelState.IsValid)
            return BadRequest();

        return Ok();
    }

    [HttpDelete(Name = "RemoveMusicFromPlaylist")]
    public async Task<IActionResult> Remove(int musicId, int playlistId)
    {
        var result = _playlistRepository.RemoveMusicFromPlaylist(playlistId, musicId);
        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(result);
    }
}