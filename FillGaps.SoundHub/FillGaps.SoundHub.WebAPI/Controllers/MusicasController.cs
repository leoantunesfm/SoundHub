using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicasController : ControllerBase
    {
        private readonly IMusicaService _musicaService;

        public MusicasController(IMusicaService musicaService)
        {
            _musicaService = musicaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarMusicaRequestDto requestDto)
        {
            try
            {
                var musicaCriada = await _musicaService.CriarMusicaAsync(requestDto);
                return Ok(musicaCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var musicas = await _musicaService.ObterTodasMusicasAsync();
            return Ok(musicas);
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> Pesquisar([FromQuery] string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                return BadRequest("O termo de busca não pode ser vazio.");
            }

            var musicas = await _musicaService.PesquisarMusicasAsync(termo);
            return Ok(musicas);
        }
    }
}
