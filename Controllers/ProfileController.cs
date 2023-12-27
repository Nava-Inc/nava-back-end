using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nava.Dto;
using Nava.Interface;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private IUserInfoRepository _userInfoRepository;
    private readonly IMapper _mapper;


    public ProfileController(ILogger<ProfileController> logger, IUserInfoRepository userInfoRepository, IMapper mapper)
    {
        _logger = logger;
        _userInfoRepository = userInfoRepository;
        _mapper = mapper;
    }

    [HttpGet(Name = "ProfileInfo")]
    [ProducesResponseType(200, Type = typeof(UserInfoDto))]
    [ProducesResponseType(400)]
    public IActionResult ProfileInfo(int id)
    {
        var profile = _mapper.Map<UserInfoDto>(_userInfoRepository.GetUserInfo(id));
        if (profile == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(profile);
    }

    [HttpPut(Name = "UpdateProfile")]
    [ProducesResponseType(200, Type = typeof(UserInfoDto))]
    [ProducesResponseType(400)]
    public IActionResult
        UpdateProfile(int id,
            [FromBody] UpdateUserInfoDto userInfoDto)
    {
        var result = _mapper.Map<UpdateUserInfoDto>(_userInfoRepository.UpdateUserInfo(id, userInfoDto));
        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest("error occured during the update.");
    }


    [HttpDelete("Musics/Remove", Name = "RemoveMusic")]
    public IActionResult RemoveMusic()
    {
        throw new NotImplementedException(); // todo: we should have a music model for this part
    }
}