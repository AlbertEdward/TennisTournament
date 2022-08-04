using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Infrastructure;
using TennisTournament.Infrastructure.Seeder;
using TennisTournament.Models.Player;
using TennisTournament.Services;
using TennisTournament.Services.Players;

namespace TennisTournament.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IUploadFileService uploadFileService;

        public PlayerController(IPlayerService players, IUploadFileService uploadFile)
        {
            this.playerService = players;
            this.uploadFileService = uploadFile;
        }

        [Authorize]
        public async Task<IActionResult> All([FromQuery] AllPlayersQueryModel query)
        {
            var queryResult = await this.playerService.All(
                query.SearchTerm,
                query.Gender);

            query.TotalPlayers = queryResult.TotalPlayers;
            query.Players = queryResult.Players;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var player = this.playerService.Details(id);
            var userId = this.User.GetId();

            return View(new PlayerServiceModel
            {
                Id = id,
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke,
                ProfilePhoto = player.ProfilePhoto,
                Tournaments = player.Tournaments,
                Challenges = player.Challenges
            });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = this.playerService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public async Task<IActionResult> EditAsync(int id)
        {
            var player = this.playerService.Details(id);

            return View(new PlayerFormModel
            {
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAsync(int id, PlayerFormModel player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            var playerIsEdited = this.playerService.Edit(
                id,
                player.Name,
                player.Age,
                player.Gender,
                player.StrongHand,
                player.BackHandStroke);

            if (!playerIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult AddPlayer()
        {
            if (!User.IsInRole(Roles.Administrator) && this.UserIsPlayer())
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public IActionResult AddPlayer(PlayerFormModel player)
        {
            if (this.UserIsPlayer() && User.IsInRole(Roles.User))
            {
                return View(player);
            }

            if (!ModelState.IsValid)
            {
                return View(player);
            }

            if (player.ProfilePhoto == null || !(player.ProfilePhoto.FileName.EndsWith(".jpg")))
            {
                return View(player);
            }

            var userId = this.User.GetId();

            string profilePhoto = this.UploadProfilePhoto(player.ProfilePhoto);

            var playerData = new Player
            {
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke,
                ProfilePhoto = profilePhoto,
                UserId = userId
            };

            this.playerService.AddPlayer(player, userId, profilePhoto);

            return RedirectToAction(nameof(All));
        }

        public bool UserIsPlayer()
        {
            var userId = this.User.GetId();

            return this.playerService.UserIsPlayer(userId);
        }

        private string UploadProfilePhoto(IFormFile profilePhoto)
        {
            var photo = this.uploadFileService.ProfilePhoto(profilePhoto);

            return photo;
        }
    }
}
