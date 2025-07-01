using FillGaps.SoundHub.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebApp.Controllers
{
    [Authorize]
    public class ArtistasController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public ArtistasController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _apiClientService.ObterDadosPaginaArtistasAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Favoritar(Guid artistaId)
        {
            await _apiClientService.FavoritarArtistaAsync(artistaId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desfavoritar(Guid artistaId)
        {
            await _apiClientService.DesfavoritarArtistaAsync(artistaId);
            return RedirectToAction("Index");
        }
    }
}
