using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models;
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
            Games = this.GetGames(),
            Rules = this.GetRules(),
            LastSets = this.GetLastSets()
        });

        [HttpPost]
        public IActionResult Add(AddTournamentFormModel tournament)
        {
            if (!this.data.GameTypes.Any(t => t.Id == tournament.GameTypeId))
            {
                this.ModelState.AddModelError(nameof(tournament.GameTypeId), "Category does not exist!");
            }


            if (!ModelState.IsValid)
            {
                tournament.GameTypes = this.GetGameTypes();
                tournament.CourtTypes = this.GetCourtTypes();
                tournament.Sets = this.GetSets();
                tournament.Games = this.GetGames();
                tournament.Rules = this.GetRules();
                tournament.LastSets = this.GetLastSets();

                return View(tournament);
            }

            var tournamentData = new Tournament
            {
                Name = tournament.Name,
                GameTypeId = tournament.GameTypeId,
                CourtTypeId = tournament.CourtTypeId,
                SetId = tournament.SetId,
                GameId = tournament.GameId,
                RuleId = tournament.RuleId,
                LastSetId = tournament.LastSetId
            };

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();

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

        private IEnumerable<ViewModel> GetGames()
            => this.data
            .Games
            .Select(t => new ViewModel
            {
                Id = t.Id,
                Name = t.Name
            });

        private IEnumerable<ViewModel> GetRules()
            => this.data
            .Rules
            .Select(t => new ViewModel
            {
                Id = t.Id,
                Name = t.Name
            });

        private IEnumerable<ViewModel> GetLastSets()
            => this.data
            .LastSets
            .Select(t => new ViewModel
            {
                Id = t.Id,
                Name = t.Name
            });
    }
}
