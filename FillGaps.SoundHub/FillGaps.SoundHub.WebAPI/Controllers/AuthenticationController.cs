using FillGaps.SoundHub.Application.DTOs.Identity;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterRequestDto requestDto)
        {
            try
            {
                await _authenticationService.RegisterAsync(requestDto);
                return Ok(new { message = "Usuário registrado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            try
            {
                var response = await _authenticationService.LoginAsync(requestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
