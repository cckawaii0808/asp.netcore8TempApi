using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        
        if (userLogin.Username != "cc" || userLogin.Password != "hh")
            return Unauthorized();

        var tokenHandler = new JwtSecurityTokenHandler();      
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userLogin.Username),
                new Claim(ClaimTypes.Role, userLogin.Role) 
            }),
            Expires = DateTime.UtcNow.AddHours(6),//6小時候過期
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        //這邊要去swagger auth api輸入 Bearer Token
        //return Ok(new { Token = tokenString });
        return Ok($"Bearer {tokenString}");
    }
}

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // 新增角色身份
}
