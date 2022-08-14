using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data.Models;
using TennisTournament.Infrastructure;
using TennisTournament.Models.Challenge;
using TennisTournament.Services.Challenges;
using TennisTournament.Services.Challenges.Models;
using TennisTournament.Services.Players;

namespace TennisTournament.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IChallengeService challengeService;
        private readonly IPlayerService playerService;

        public ChallengeController(IChallengeService challenge, IPlayerService player)
        {
            this.challengeService = challenge;
            this.playerService = player;
        }

        [HttpGet]
        public IActionResult AddPlayerToChallenge(int id, int challengeId)
        {
            this.challengeService.AddPlayerToChallenge(this.User.GetId(), id, challengeId);

            return RedirectToAction("All", "Player");
        }

        [HttpGet]
        public IActionResult DeleteChallenge(int id)
        {
            this.challengeService.DeleteChallenge(id);

            return RedirectToAction("All", "Player");
        }

        [Authorize]
        public IActionResult CreateChallenge()
        {
            if (!UserIsPlayer())
            {
                return RedirectToAction("AddPlayer", "Player");
            }

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

            var guestId = id;
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
                PlayerGuestId = guestId
            };

            this.challengeService.CreateChallenge(challenge, guestId, hostUserId);

            return RedirectToAction("All", "Player");
        }

        public IActionResult Details(int id)
        {
            var challenge = this.challengeService.Details(id);

            return View(new ChallengeServiceModel
            {
                Id = challenge.Id,
                Name = challenge.Name,
                CourtType = challenge.CourtType,
                Game = challenge.Game,
                Set = challenge.Set,
                Rule = challenge.Rule,
                LastSet = challenge.LastSet,
                Description = challenge.Description,
                Players = challenge.Players
            });
        }

        [Authorize]
        public IActionResult ChallengeResult(int winnerId, int loserId)
        {
            this.challengeService.ChallengeResult(winnerId, loserId);

            return RedirectToAction("All", "Player");
        }

        public bool UserIsPlayer()
        {
            var userId = this.User.GetId();

            return this.playerService.UserIsPlayer(userId);
        }
    }
}
