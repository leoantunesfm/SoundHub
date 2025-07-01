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

            var token = await _apiClientService.LoginAsync(model);

            if (!string.IsNullOrWhiteSpace(token))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddHours(8),
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax,
                    Secure = true
                };
                Response.Cookies.Append("jwt", token, cookieOptions);

                return RedirectToAction("PostLoginCheck");
            }

            ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PostLoginCheck()
        {
            var assinatura = await _apiClientService.ObterMinhaAssinaturaAsync();

            if (assinatura == null)
            {
                return RedirectToAction("Index", "Subscription");
            }

            return RedirectToAction("Index", "Dashboard");
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
