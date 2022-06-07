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
            GameTypes = this.GetGameTypes(),
            CourtTypes = this.GetCourtTypes(),
            Sets = this.GetSets(),

        });

        [HttpPost]
        public IActionResult Add(AddTournamentFormModel tournament)
        {
            if (!ModelState.IsValid)
            {
                tournament.GameTypes = this.GetGameTypes();
                tournament.CourtTypes = this.GetCourtTypes();
                tournament.Sets = this.GetSets();

                return View(tournament);
            }

            return RedirectToAction("Index", "Home");
        }
        private IEnumerable<ViewModel> GetGameTypes()
            => this.data
            .GameTypes
            .Select(t => new ViewModel
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToList();


        private IEnumerable<ViewModel> GetCourtTypes()
            => this.data
            .CourtTypes
            .Select(t => new ViewModel
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToList();

        private IEnumerable<ViewModel> GetSets()
            => this.data
            .Sets
            .Select(t => new ViewModel
            {
                Id = t.Id,
                Name = t.Name
            });
    }
}
