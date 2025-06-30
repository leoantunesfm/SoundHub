using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistasController : ControllerBase
    {
        private readonly IArtistaService _artistaService;

        public ArtistasController(IArtistaService artistaService)
        {
            _artistaService = artistaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarArtistaRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var artistaCriado = await _artistaService.CriarArtistaAsync(requestDto);
                
                return CreatedAtAction(nameof(ObterPorId), new { id = artistaCriado.Id }, artistaCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var artistas = await _artistaService.ObterTodosArtistasAsync();
            return Ok(artistas);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var artista = await _artistaService.ObterArtistaPorIdAsync(id);

            if (artista == null)
            {
                return NotFound();
            }

            return Ok(artista);
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> Pesquisar([FromQuery] string termo)
        {
            var artistas = await _artistaService.PesquisarArtistasPorTermoAsync(termo);
            return Ok(artistas);
        }
    }
}
