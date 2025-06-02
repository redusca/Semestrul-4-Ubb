using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace restApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;
        public AuthController(ILoginService log, IConfiguration configuration)
        {
            _loginService = log;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] ArbitruDTO dto)
        {
            Arbitru user = null;
            try
            {
                user = _loginService.login(dto.Nume, dto.Parola);
                var token = GenerateJwtToken(user);
                return Ok(new { token, user = new { user.Id, user.Nume, user.Username } });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        private string GenerateJwtToken(Arbitru user)
        {
            var secretKey = _configuration["Jwt:SecretKey"] ?? "your_default_secret_key_at_least_32_characters_long";
            var issuer = _configuration["Jwt:Issuer"] ?? "your_issuer";
            var audience = _configuration["Jwt:Audience"] ?? "your_audience";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nume),
                new Claim("username", user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


    public class ArbitruDTO
    {
        public string Nume { get; set; }
        public string Parola { get; set; }

        public ArbitruDTO() { }

        public ArbitruDTO(string nume, string parola)
        {
            Nume = nume;
            Parola = parola;
        }
    }

}