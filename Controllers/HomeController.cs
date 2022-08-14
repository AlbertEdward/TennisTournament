using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Models.Tournament;
using TennisTournament.Services.Statistics;

namespace TennisTournament.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}