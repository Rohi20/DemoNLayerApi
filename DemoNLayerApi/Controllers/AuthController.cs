using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Business.Services;
using DemoNLayerApi.DTOs.RequestDTOs;
using DemoNLayerApi.DTOs.ResponseDTOs;
using DemoNLayerApi.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoNLayerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            User user = await _userService.AuthenticateUser(loginRequest.Email, loginRequest.Password);

            var token = GenerateToken(user);

            var response = new LoginResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Profile.Email,
                Token = token,
            };

            return Ok(response);
        }

        private string GenerateToken(User user)
        {
            var jwtConfig = _configuration.GetSection("JWTSettings");

            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);
            var issuer = jwtConfig["Issuer"];
            var audience = jwtConfig["Audience"];
            var expiration = jwtConfig["ExpiryMinutes"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
           {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Profile.Role)
            }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(expiration)),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(key),
               SecurityAlgorithms.HmacSha256Signature
           )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
