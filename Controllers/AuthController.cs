using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nava.Interface;

namespace Nava.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, IUserInfoRepository userInfoRepository)
    {
        _logger = logger;
        _userInfoRepository = userInfoRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = _userInfoRepository.AuthenticateUser(model.Username, model.Password);
        if (user == null)
        {
            return Unauthorized(); // Return 401 Unauthorized if authentication fails
        }

        // Create claims for the user
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.AccountType.ToString())
            // Add more claims as needed, such as user role, permissions, etc.
        };

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyWithTheLengthOfMoreThan32Characters");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // todo: Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new {Token = tokenString}); // Return the generated token
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
    {
        // todo: implement
        // Check if user already exists
        if (_userInfoRepository.UserExists(model.Username) != null)
        {
            return Conflict("User already exists"); // Return 409 Conflict if user exists
        }

        var newUser = _userInfoRepository.CreateUser(model.Username, model.Password, model.Email, model.AccountType);
        return Ok(newUser);
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // make the token expired by returning it in the response with an earlier date:
        var expiredToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(expires: DateTime.UtcNow));
        return Ok(new {Token = expiredToken, Message = "Logged out successfully"});
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SignUpModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int AccountType { get; set; }
    }
}