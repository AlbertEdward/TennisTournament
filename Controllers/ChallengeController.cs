using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data.Models;
using TennisTournament.Infrastructure;
using TennisTournament.Models.Challenge;
using TennisTournament.Services.Challenges;
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
        public IActionResult RemovePlayerFromChallenge(int challengeId)
        {
            this.challengeService.RemovePlayerFromChallenge(challengeId);

            return RedirectToAction("All", "Player");
        }

        [Authorize]
        public async Task<IActionResult> CreateChallenge()
        {
            if (!UserIsPlayer())
            {
                return RedirectToAction("AddPlayer", "Player");
            }

            return View(new ChallengeFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateChallenge(ChallengeFormModel challenge, int guestId)
        {
            if (!ModelState.IsValid)
            {
                return View(challenge);
            }

            if (!UserIsPlayer())
            {
                return RedirectToAction("Add", "Player");
            }

            var hostUserId = this.User.GetId();

            var challengeData = new Challenge
            {
                Id = challenge.Id,
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

            var challengeId = challenge.Id;

            this.challengeService.CreateChallenge(challenge, guestId, hostUserId);
            this.challengeService.AddPlayerToChallenge(hostUserId, guestId, challengeId);

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

        public bool UserIsPlayer()
        {
            var userId = this.User.GetId();

            return this.playerService.UserIsPlayer(userId);
        }
    }
}
