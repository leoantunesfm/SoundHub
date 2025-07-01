using FillGaps.SoundHub.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FillGaps.SoundHub.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }
    }
}
