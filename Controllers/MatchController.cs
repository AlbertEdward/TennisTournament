using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TennisTournament.Controllers
{
    public class MatchController : Controller
    {
        [Authorize]
        public IActionResult CreateMatch()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateMatch(ChallengeFormModel challenge, int id)
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
    }
}
