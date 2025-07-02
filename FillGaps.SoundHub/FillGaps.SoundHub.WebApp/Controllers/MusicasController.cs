using FillGaps.SoundHub.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebApp.Controllers
{
    [Authorize]
    public class MusicasController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public MusicasController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task<IActionResult> Index(string termoBusca)
        {
            var viewModel = await _apiClientService.ObterDadosPaginaMusicasAsync(termoBusca);

            ViewData["TermoBuscaAtual"] = termoBusca;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Favoritar(Guid musicaId)
        {
            await _apiClientService.FavoritarMusicaAsync(musicaId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desfavoritar(Guid musicaId)
        {
            await _apiClientService.DesfavoritarMusicaAsync(musicaId);
            return RedirectToAction("Index");
        }
    }
}
