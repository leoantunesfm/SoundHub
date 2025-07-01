using FillGaps.SoundHub.Application.DTOs.Billing;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanoService _planoService;

        public PlanosController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPlanoRequestDto requestDto)
        {
            try
            {
                var plano = await _planoService.CriarPlanoAsync(requestDto);
                return CreatedAtAction(nameof(Criar), new { id = plano.Id }, plano);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("ativos")]
        public async Task<IActionResult> ObterAtivos()
        {
            var planos = await _planoService.ObterPlanosAtivosAsync();
            return Ok(planos);
        }
    }
}
