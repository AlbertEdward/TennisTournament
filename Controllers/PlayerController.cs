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
        private readonly IUploadFileService uploadFileService;
        private readonly IPlayerService playerService;
        private readonly TennisDbContext data;

        public PlayerController(TennisDbContext data, IPlayerService players, IUploadFileService uploadFileService)
        {
            this.data = data;
            this.playerService = players;
            this.uploadFileService = uploadFileService;
        }

        public IActionResult Invite(int id)
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
        public IActionResult All([FromQuery] AllPlayersQueryModel query)
        {
            var queryResult = this.playerService.All(
                query.SearchTerm,
                query.Gender);

            query.TotalPlayers = queryResult.TotalPlayers;
            query.Players = queryResult.Players;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var player = this.playerService.Details(id);

            return View(new AllPlayersQueryModel
            {
                Id = id,
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke,
                ProfilePhoto = player.ProfilePhoto
            }) ;
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var deleted = this.playerService.Delete(id);

            return RedirectToAction(nameof(All));
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

        [Authorize]
        public IActionResult Add()
        {
            if (!User.IsInRole(Roles.Administrator))
            {
                if (this.UserIsPlayer())
                {
                    return BadRequest();
                }
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public IActionResult Add(PlayerFormModel player)
        {
            if (this.UserIsPlayer() && User.IsInRole(Roles.User))
            {
                return BadRequest();
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

            var userIsAlreadyCompetitor = this.data
                .Players
                .Any(p => p.UserId == userId);

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

            this.data.Players.Add(playerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsPlayer()
            => this.data.Players.Any(p => p.UserId == this.User.GetId());

        private string UploadProfilePhoto(IFormFile profilePhoto)
        {
            var photo = this.uploadFileService.ProfilePhoto(profilePhoto);

            return photo;
        }
    }
}
