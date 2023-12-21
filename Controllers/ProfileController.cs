using Microsoft.AspNetCore.Mvc;

namespace Nava.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "ProfileInfo")]
    public IActionResult ProfileInfo()
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "UpdateProfile")]
    public IActionResult UpdateProfile() // todo: we should have a profile model for this part
    {
        throw new NotImplementedException();
    }

    [HttpPost("Musics/Upload", Name = "UploadMusic")]
    public IActionResult UploadMusic()
    {
        throw new NotImplementedException(); // todo: we should have a music model for this part
    }

    [HttpDelete("Musics/Remove", Name = "RemoveMusic")]
    public IActionResult RemoveMusic()
    {
        throw new NotImplementedException(); // todo: we should have a music model for this part
    }
}