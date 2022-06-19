using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Models.Tournament;
using TennisTournament.Services.Statistics;

namespace TennisTournament.Controllers
{
    public class HomeController : Controller
    {
        private readonly TennisDbContext data;

        public HomeController(TennisDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}