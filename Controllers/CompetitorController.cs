using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Infrastructure;
using TennisTournament.Models.Player;
using TennisTournament.Services;
using TennisTournament.Services.Players;

namespace TennisTournament.Controllers
{
    public class CompetitorController : Controller
    {
        private readonly TennisDbContext data;
        private readonly IPlayerService playerService;
        private readonly IUploadFileService uploadFileService;

        public CompetitorController(IPlayerService playerService, IUploadFileService uploadFileService, TennisDbContext data)
        {
            this.uploadFileService = uploadFileService;
            this.playerService = playerService;
            this.data = data;
        }

        [Authorize]
        public IActionResult MyProfile() => View();

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(PlayerFormModel competitor)
        {
            var userId = this.User.GetId();

            var userIsAlreadyCompetitor = this.data
                .Players
                .Any(p => p.UserId == userId);

            if (userIsAlreadyCompetitor)
            {
                //TODO
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(competitor);
            }

            string profilePhoto = this.UploadProfilePhoto(competitor.ProfilePhoto);

            var competitorData = new Player
            {
                Id = competitor.Id,
                Name = competitor.Name,
                Age = competitor.Age,
                Gender = competitor.Gender,
                StrongHand = competitor.StrongHand,
                BackHandStroke = competitor.BackHandStroke,
                ProfilePhoto = profilePhoto,
                UserId = userId,
            };

            this.data.Players.Add(competitorData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Player");
        }
        private string UploadProfilePhoto(IFormFile profilePhoto)
        {
            var photo = this.uploadFileService.ProfilePhoto(profilePhoto);

            return photo;
        }
    }
}
