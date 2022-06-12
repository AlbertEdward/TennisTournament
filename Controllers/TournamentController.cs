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
        

        public IActionResult All()
        {
            var tournaments = this.data
                .Tournaments
                .OrderByDescending(t => t.Id)
                .Select(t => new TournamentListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    GameType = t.GameType,
                    CourtType = t.CourtType,
                })
                .ToList();

            return View(tournaments);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTournamentFormModel tournament)
        {
            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            //if (dealerId == 0)
            //{
            //    return RedirectToAction(nameof(DealerController.Create), "Dealers");
            //}

            //if (!this.UserIsDealer())
            //{
            //    return RedirectToAction(nameof(DealerController.Create), "Dealers");
            //}

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
                //DealerId = dealerId
            };

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsDealer()
            =>!this
            .data
            .Dealers
            .Any(d => d.UserId == this.User.GetId());
    }
}
