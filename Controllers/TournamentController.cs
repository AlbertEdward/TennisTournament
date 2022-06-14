using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Infrastructure;
using TennisTournament.Models.Tournament;

namespace TennisTournament.Controllers
{
    public class TournamentController : Controller
    {
        private readonly TennisDbContext data;

        public TournamentController(TennisDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Add()
        {
            return View(new AddTournamentFormModel());
        }
        

        public IActionResult All(CourtType courtType,GameType gameType,string searchTerm)
        {
            var tournamentsQuery = this.data.Tournaments.AsQueryable();

            if (courtType != CourtType.Select)
            {
                tournamentsQuery = tournamentsQuery.Where(c => c.CourtType == courtType);
            }

            if (gameType != GameType.Select)
            {
                tournamentsQuery = tournamentsQuery.Where(g => g.GameType == gameType);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                tournamentsQuery = tournamentsQuery.Where(t =>
                    t.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    t.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            var tournaments = tournamentsQuery
                .OrderByDescending(t => t.Id)
                .Select(t => new TournamentListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    GameType = t.GameType,
                    CourtType = t.CourtType,
                })
                .ToList();

            return View(new AllTournamentsQueryModel
            {
                Tournaments = tournaments,
                CourtTypes = courtType,
                GameType = gameType,
                SearchTerm = searchTerm,
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTournamentFormModel tournament)
        {
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }

            var tournamentData = new Tournament
            {
                Name = tournament.Name,
                GameType = tournament.GameTypes,
                CourtType = tournament.CourtTypes,
                Sets = tournament.Sets,
                Games = tournament.Games,
                Rules = tournament.Rules,
                LastSets = tournament.LastSets,
                Description = tournament.Description,
            };

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
