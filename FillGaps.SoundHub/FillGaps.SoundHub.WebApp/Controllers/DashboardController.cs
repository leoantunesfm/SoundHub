using FillGaps.SoundHub.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FillGaps.SoundHub.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public DashboardController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            ViewData["UserName"] = User.FindFirst(ClaimTypes.Name)?.Value;
            var favoritos = await _apiClientService.ObterMeusFavoritosAsync();
            return View(favoritos);
        }
    }
}
