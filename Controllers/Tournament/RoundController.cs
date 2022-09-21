using Microsoft.AspNetCore.Mvc;

namespace TennisTournament.Controllers
{
    public class RoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
