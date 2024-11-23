using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockApp.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IConfiguration _configuration;

        public TokenController(ILogger<TokenController> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Tentativa de login para o usuário: {Username}", userLoginDto.Username);


                if (!ValidateUserCredentials(userLoginDto.Username, userLoginDto.Password))
                {
                    _logger.LogWarning("Falha na autenticação para o usuário: {Username}", userLoginDto.Username);
                    return Unauthorized(new { Message = "Credenciais inválidas." });
                }

                var token = GenerateJwtToken(userLoginDto.Username);

                _logger.LogInformation("Login bem-sucedido para o usuário: {Username}", userLoginDto.Username);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar login para o usuário: {Username}", userLoginDto.Username);
                return StatusCode(500, new { Message = "Erro interno no servidor", Details = "Ocorreu um erro ao processar sua solicitação." });
            }
        }

        private bool ValidateUserCredentials(string username, string password)
        {

            return username == "admin" && password == "password";
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}