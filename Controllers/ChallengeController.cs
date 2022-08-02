using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data.Models;
using TennisTournament.Infrastructure;
using TennisTournament.Models.Challenge;
using TennisTournament.Services;
using TennisTournament.Services.Challenges;
using TennisTournament.Services.Players;
using TennisTournament.Services.Tournaments;

namespace TennisTournament.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IChallengeService challengeService;

        public ChallengeController(IChallengeService challenge)
        {
            this.challengeService = challenge;
        }

        [HttpGet]
        public IActionResult AddPlayerToChallenge(int id)
        {
            this.challengeService.AddPlayerToChallenge(this.User.GetId(), id);

            return RedirectToAction("All", "Player");
        }

        [HttpGet]
        public IActionResult RemovePlayerFromChallenge(int id)
        {
            this.challengeService.RemovePlayerFromChallenge(this.User.GetId(), id);

            return RedirectToAction("All", "Player");
        }

        [Authorize]
        public async Task<IActionResult> CreateChallenge()
        {
            return View(new ChallengeFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateChallenge(ChallengeFormModel challenge, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(challenge);
            }

            var hostUserId = this.User.GetId();

            var challengeData = new Challenge
            {
                Name = challenge.Name,
                CourtType = challenge.CourtTypes,
                Sets = challenge.Sets,
                Games = challenge.Games,
                Rules = challenge.Rules,
                LastSets = challenge.LastSets,
                Description = challenge.Description,
                PlayerHostUserId = hostUserId,
                PlayerGuestId = id
            };

            this.challengeService.CreateChallenge(challenge, id);

            return RedirectToAction("All", "Player");
        }

        public IActionResult Details(int id)
        {
            var challenge = this.challengeService.Details(id);

            return View(new AllChallengesQueryModel
            {
                Id = challenge.Id,
                Name = challenge.Name,
                CourtTypes = challenge.CourtTypes,
                Games = challenge.Games,
                Sets = challenge.Sets,
                Rules = challenge.Rules,
                LastSets = challenge.LastSets,
                Description = challenge.Description
            });
        }
    }
}
