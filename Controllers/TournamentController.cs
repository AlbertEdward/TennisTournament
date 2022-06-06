using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Models.Tournament;

namespace TennisTournament.Controllers
{
    public class TournamentController : Controller
    {
        private readonly TennisDbContext data;

        public TournamentController(TennisDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddTournamentFormModel
        {
            TypeOfGames = this.GetTypeOfGames()
        });

        [HttpPost]
        public IActionResult Add(AddTournamentFormModel tournament)
        {
            if (!ModelState.IsValid)
            {
                tournament.TypeOfGames = this.GetTypeOfGames();
                return View(tournament);
            }

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<TypeOfGameViewModel> GetTypeOfGames()
            => this.data
            .TypeOfGames
            .Select(t => new TypeOfGameViewModel
            {
                Id = t.Id,
                Name = t.Name,
            })
            .ToList();
    }
}
