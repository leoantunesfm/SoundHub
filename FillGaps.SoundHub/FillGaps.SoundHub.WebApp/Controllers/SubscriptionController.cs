using FillGaps.SoundHub.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebApp.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public SubscriptionController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task<IActionResult> Index()
        {
            var planos = await _apiClientService.ObterPlanosAtivosAsync();
            return View(planos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(Guid planoId)
        {
            if (planoId == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Por favor, selecione um plano válido.";
                return RedirectToAction("Index");
            }

            var success = await _apiClientService.CriarAssinaturaAsync(planoId);

            if (success)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["ErrorMessage"] = "Ocorreu um erro ao processar sua assinatura. Tente novamente.";
            return RedirectToAction("Index");
        }
    }
}
