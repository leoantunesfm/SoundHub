using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbunsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbunsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarAlbumRequestDto requestDto)
        {
            try
            {
                var albumCriado = await _albumService.CriarAlbumAsync(requestDto);
                return Ok(albumCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("por-artista/{artistaId:guid}")]
        public async Task<IActionResult> ObterPorArtista(Guid artistaId)
        {
            var albuns = await _albumService.ObterAlbunsPorArtistaAsync(artistaId);
            return Ok(albuns);
        }
    }
}
