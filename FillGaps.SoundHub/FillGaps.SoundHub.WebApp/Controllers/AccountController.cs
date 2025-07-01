using FillGaps.SoundHub.WebApp.Models.Account;
using FillGaps.SoundHub.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FillGaps.SoundHub.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public AccountController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _apiClientService.LoginAsync(model);

            if (success)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _apiClientService.RegisterAsync(model);

            if (success)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Ocorreu um erro ao tentar registrar. Verifique os dados ou tente novamente mais tarde.");
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Index", "Home");
        }
    }
}
