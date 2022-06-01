using Microsoft.AspNetCore.Mvc;
using TennisTournament.Models.Tournament;

namespace TennisTournament.Controllers
{
    public class TournamentController : Controller
    {
        
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddTournamentFormModel tournament)
        {
            return View();
        }
    }
}
