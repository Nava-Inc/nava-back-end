using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nava.Dto;
using Nava.Entities;
using Nava.Interface;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MusicController : ControllerBase
{
    private readonly ILogger<MusicController> _logger;
        private readonly IMusicRepository _musicRepository;
    private readonly IUserInteractionsRepository _userInteractionRepository;
    private readonly IMapper _mapper;

    public MusicController(ILogger<MusicController> logger, IMusicRepository musicRepository,
        IUserInteractionsRepository userInteractionsRepository, IMapper mapper)
    {
        _logger = logger;
        _musicRepository = musicRepository;
        _userInteractionRepository = userInteractionsRepository;
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
    [Consumes("multipart/form-data")]
    [ProducesResponseType(200, Type = typeof(UploadMusicDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(415)] // Unsupported Media Type
    public async Task<IActionResult> UploadMusic(int userId, [FromForm] UploadMusicDto musicDto)
    {
        var result = await _musicRepository.UploadMusic(userId, musicDto);
        if (result == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPatch("like", Name = "ToggleLike")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult ToggleLike(int userId, int musicId)
    {
        try
        {
            var result = _userInteractionRepository.ToggleLike(userId, musicId);
            return result ? Ok() : BadRequest();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }


    [HttpPost("comment", Name = "Comment")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult Comment(int userId, int musicId, [FromBody] string comment)
    {
        var result = _userInteractionRepository.PostComment(userId, musicId, comment);
        if (!result || !ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok($"comment posted successfully by {userId} at {DateTime.Now}. {comment}");
    }
    
    [HttpDelete("remove", Name = "RemoveMusic")]
    public IActionResult RemoveMusic(int id)
    {
        var result = _musicRepository.DeleteMusic(id);
        if (result == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}