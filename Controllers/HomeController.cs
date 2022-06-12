using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Models.Tournament;

namespace TennisTournament.Controllers
{
    public class HomeController : Controller
    {
        private readonly TennisDbContext data;

        public HomeController(TennisDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            //var tournaments = this.data
            //    .Tournaments
            //    .OrderByDescending(p => p.Id)
            //    .Select(t => new TournamentListingViewModel
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //        GameType = t.GameType,
            //        CourtType = t.CourtType,
            //        Description = t.Description
            //    })
            //    .Take(3)
            //    .ToList();

            return View();
        }
    }
}