using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
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
        private readonly TennisDbContext data;
        private readonly IChallengeService challengeService;
        private readonly ITournamentService tournamentService;
        private readonly IPlayerService playerService;
        private readonly IUploadFileService uploadFileService;

        public ChallengeController(
            TennisDbContext data,
            IChallengeService challenge,
            ITournamentService tournaments,
            IPlayerService playerService,
            IUploadFileService uploadFile)
        {
            this.data = data;
            this.challengeService = challenge;
            this.tournamentService = tournaments;
            this.playerService = playerService;
            this.uploadFileService = uploadFile;
        }

        [HttpGet]
        public IActionResult AddPlayerToChallenge(int challengeId)
        {
            this.challengeService.AddPlayerToChallenge(this.User.GetId(), challengeId);

            return RedirectToAction("All", "Player");
        }

        [HttpGet]
        public IActionResult RemovePlayerFromChallenge(int challengeId)
        {
            this.challengeService.RemovePlayerFromChallenge(this.User.GetId(), challengeId);

            return RedirectToAction("All", "Player");

        }

        [Authorize]
        public async Task<IActionResult> AddChallenge()
        {
            return View(new ChallengeFormModel());
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public async Task<IActionResult> AddChallenge(ChallengeFormModel challenge)
        {
            if (!ModelState.IsValid)
            {
                return View(challenge);
            }

            var userId = this.User.GetId();

            var challengeData = new Challenge
            {
                Name = challenge.Name,
                CourtType = challenge.CourtTypes,
                Sets = challenge.Sets,
                Games = challenge.Games,
                Rules = challenge.Rules,
                LastSets = challenge.LastSets,
                Description = challenge.Description,
                PlayerHostId = userId
            };

            this.data.Challenges.Add(challengeData);
            this.data.SaveChangesAsync();

            return RedirectToAction("All", "Player");
        }
    }
}
