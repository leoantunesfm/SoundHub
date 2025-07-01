using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FillGaps.SoundHub.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["UserName"] = User.FindFirst(ClaimTypes.Name)?.Value;
            return View();
        }
    }
}
