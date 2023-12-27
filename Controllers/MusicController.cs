using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nava.Dto;
using Nava.Entities;
using Nava.Interface;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
public class MusicController : ControllerBase
{
    private readonly ILogger<MusicController> _logger;
    private readonly IMusicRepository _musicRepository;
    private readonly IMapper _mapper;

    public MusicController(ILogger<MusicController> logger, IMusicRepository musicRepository, IMapper mapper)
    {
        _logger = logger;
        _musicRepository = musicRepository;
        _mapper = mapper;
    }

    [HttpGet("musicInfo", Name = "MusicInfo")]
    [ProducesResponseType(200, Type = typeof(Music))]
    [ProducesResponseType(400)]
    public IActionResult MusicInfo(int id)
    {
        if (!_musicRepository.MusicExists(id))
        {
            return NotFound();
        }

        var music = _musicRepository.GetMusicInfo(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(music);
    }


    [HttpGet("download", Name = "Download")]
    [ProducesResponseType(200, Type = typeof(File))]
    [ProducesResponseType(400)]
    public IActionResult Download(int id)
    {
        var musicContent = _musicRepository.GetMusicContent(id);
        if (musicContent == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return File(musicContent.FileContent, "audio/mpeg", musicContent.Name);
    }

    [HttpPost("upload", Name = "UploadMusic")]
    [ProducesResponseType(200, Type = typeof(UploadMusicDto))]
    [ProducesResponseType(400)]
    public IActionResult UploadMusic(int userId, [FromBody] UploadMusicDto musicDto)
    {
        var result = _musicRepository.UploadMusic(userId, musicDto);
        if (result == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(result);
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