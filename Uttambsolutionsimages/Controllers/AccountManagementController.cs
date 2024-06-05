using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Uttambsolutionsimages.Models;
using Uttambsolutionsimages.NewFolder;

namespace Uttambsolutionsimages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountManagementController : ControllerBase
    {
        IConfiguration _config;
        public AccountManagementController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [Route("Authenticate"), HttpPost]
        public async Task<ActionResult> AuthenticateAsync([FromBody] Usercred userdata)
        {
            if (userdata.username =="info@uttambsolutions.com" && userdata.password == "Password@123!")
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", "100"),
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn);
                return Ok(new UsermodelResponce
                {
                    Respstatus = 200,
                    RespMessage = "Authorized",
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            else
            {
                return Unauthorized(new UsermodelResponce
                {
                    Respstatus = 401,
                    RespMessage = "Unauthorized",
                    Token = ""
                });
            }
        }
    }
}
