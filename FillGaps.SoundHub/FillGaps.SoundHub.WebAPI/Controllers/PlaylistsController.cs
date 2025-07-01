using FillGaps.SoundHub.Application.DTOs.UserEngagement;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPlaylistRequestDto requestDto)
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null)
            {
                return Unauthorized("Não foi possível identificar o usuário.");
            }

            try
            {
                var playlistCriada = await _playlistService.CriarPlaylistAsync(requestDto, usuarioId.Value);
                return Ok(playlistCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("adicionar-musica")]
        public async Task<IActionResult> AdicionarMusica([FromBody] AdicionarMusicaPlaylistRequestDto requestDto)
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null)
            {
                return Unauthorized("Não foi possível identificar o usuário.");
            }

            try
            {
                await _playlistService.AdicionarMusicaAPlaylistAsync(requestDto, usuarioId.Value);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private Guid? ObterUsuarioIdLogado()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(idClaim, out var usuarioId))
            {
                return usuarioId;
            }
            return null;
        }
    }
}
