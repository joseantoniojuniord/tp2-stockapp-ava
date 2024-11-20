using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public TokenController(IAuthenticationService authService)
        {
            _authService = authService;
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
                var token = await _authService.AuthenticateAsync(userLoginDto.Username, userLoginDto.Password);

                if (token == null)
                {
                    return Unauthorized(new { Message = "Credenciais inválidas." });
                }

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Erro interno no servidor", Details = ex.Message });
            }
        }
    }
}
