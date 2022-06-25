using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Player;
using TennisTournament.Services;
using TennisTournament.Services.Players;

namespace TennisTournament.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IUploadFileService uploadFileService;
        private readonly IPlayerService playerService;
        private readonly TennisDbContext data;

        public PlayerController(TennisDbContext data, IPlayerService players, IUploadFileService uploadFileService)
        {
            this.data = data;
            this.playerService = players;
            this.uploadFileService = uploadFileService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new PlayerFormModel());
        }

        [Authorize]
        public IActionResult Edit(int id)
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

        public IActionResult Details(int id)
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


        [Authorize]
        public IActionResult Delete(int id)
        {
            var deleted = this.playerService.Delete(id);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All([FromQuery] AllPlayersQueryModel query)
        {
            var queryResult = this.playerService.All(
                query.SearchTerm,
                query.Gender);

            query.TotalPlayers = queryResult.TotalPlayers;
            query.Players = queryResult.Players;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PlayerFormModel player)
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

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public IActionResult Add(PlayerFormModel player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            if (player.ProfilePhoto == null || !(player.ProfilePhoto.FileName.EndsWith(".jpg")))
            {
                return View(player);
            }

            string profilePhoto = this.UploadProfilePhoto(player.ProfilePhoto);

            var playerData = new Player
            {
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke,
                ProfilePhoto = profilePhoto
            };

            this.data.Players.Add(playerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private string UploadProfilePhoto(IFormFile profilePhoto)
        {
            var photo = this.uploadFileService.ProfilePhoto(profilePhoto);

            return photo;
        }
    }
}
