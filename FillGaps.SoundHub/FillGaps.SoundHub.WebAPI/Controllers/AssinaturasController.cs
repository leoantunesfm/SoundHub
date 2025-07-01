using FillGaps.SoundHub.Application.DTOs.Billing;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssinaturasController : ControllerBase
    {
        private readonly IAssinaturaService _assinaturaService;

        public AssinaturasController(IAssinaturaService assinaturaService)
        {
            _assinaturaService = assinaturaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarAssinaturaRequestDto requestDto)
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null)
            {
                return Unauthorized("Não foi possível identificar o usuário.");
            }

            try
            {
                var assinatura = await _assinaturaService.CriarAssinaturaAsync(requestDto, usuarioId.Value);
                return Ok(assinatura);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("minha-assinatura")]
        public async Task<IActionResult> ObterMinhaAssinatura()
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null)
            {
                return Unauthorized("Não foi possível identificar o usuário.");
            }

            var assinatura = await _assinaturaService.ObterAssinaturaPorUsuarioIdAsync(usuarioId.Value);
            if (assinatura == null)
            {
                return NotFound("Nenhuma assinatura encontrada para este usuário.");
            }

            return Ok(assinatura);
        }

        private Guid? ObterUsuarioIdLogado()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(idClaim, out var usuarioId) ? usuarioId : null;
        }
    }
}
