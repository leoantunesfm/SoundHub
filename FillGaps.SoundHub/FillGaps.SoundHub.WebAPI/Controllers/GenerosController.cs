using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GenerosController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarGeneroRequestDto requestDto)
        {
            try
            {
                var genero = await _generoService.CriarGeneroAsync(requestDto);
                return CreatedAtAction(nameof(Criar), new { id = genero.Id }, genero);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var generos = await _generoService.ObterTodosGenerosAsync();
            return Ok(generos);
        }
    }
}
