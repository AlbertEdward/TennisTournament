using Microsoft.AspNetCore.Mvc;

namespace TennisTournament.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
