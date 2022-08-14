using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data.Models;
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

            var matchData = new Match
            {
                FirstPlayerId = match.FirstPlayerId,
                SecondPlayerId = match.SecondPlayerId,
                TournamentId = id
            };

            this.matchService.CreateMatch(match, id);

            return RedirectToAction("All", "Tournament");
        }
    }
}
