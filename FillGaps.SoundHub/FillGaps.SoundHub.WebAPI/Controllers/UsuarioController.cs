using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("favoritos")]
        public async Task<IActionResult> ObterFavoritos()
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null) return Unauthorized();

            var favoritos = await _usuarioService.ObterFavoritosAsync(usuarioId.Value);
            return Ok(favoritos);
        }

        [HttpPost("favoritos/musicas/{musicaId:guid}")]
        public async Task<IActionResult> FavoritarMusica(Guid musicaId)
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null) return Unauthorized();

            await _usuarioService.FavoritarMusicaAsync(usuarioId.Value, musicaId);
            return Ok();
        }

        [HttpDelete("favoritos/musicas/{musicaId:guid}")]
        public async Task<IActionResult> DesfavoritarMusica(Guid musicaId)
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null) return Unauthorized();

            await _usuarioService.DesfavoritarMusicaAsync(usuarioId.Value, musicaId);
            return NoContent();
        }

        [HttpPost("favoritos/artistas/{artistaId:guid}")]
        public async Task<IActionResult> FavoritarArtista(Guid artistaId)
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null) return Unauthorized();

            await _usuarioService.FavoritarArtistaAsync(usuarioId.Value, artistaId);
            return Ok();
        }

        [HttpDelete("favoritos/artistas/{artistaId:guid}")]
        public async Task<IActionResult> DesfavoritarArtista(Guid artistaId)
        {
            var usuarioId = ObterUsuarioIdLogado();
            if (usuarioId == null) return Unauthorized();

            await _usuarioService.DesfavoritarArtistaAsync(usuarioId.Value, artistaId);
            return NoContent();
        }

        private Guid? ObterUsuarioIdLogado()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(idClaim, out var usuarioId) ? usuarioId : null;
        }
    }
}
