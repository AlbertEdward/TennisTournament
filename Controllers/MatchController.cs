using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data.Models;
using TennisTournament.Models.Match;
using TennisTournament.Services.Matches;
using TennisTournament.Services.Matches.Models;

namespace TennisTournament.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService matchService;

        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [Authorize]
        public IActionResult CreateMatch(MatchServiceModel match, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(match);
            }

            var tournamentId = id;

            var matchData = new Match
            {
                FirstPlayerId = match.FirstPlayerId,
                SecondPlayerId = match.SecondPlayerId,
                TournamentId = tournamentId
            };

            this.matchService.CreateMatch(match, id);

            return RedirectToAction("All", "Tournament");
        }

        [Authorize]
        public IActionResult ResultMatch(MatchWinnerFormModel winnerModel, int matchId)
        {
            this.matchService.MatchResult(matchId, winnerModel.WinnerId);

            return RedirectToAction("All", "Player");
        }
    }
}
