using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Player;
using TennisTournament.Services.Players;

namespace TennisTournament.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService players;
        private readonly TennisDbContext data;

        public PlayerController(IPlayerService players, TennisDbContext data)
        {
            this.data = data;
            this.players = players;
        }

        [Authorize] 
        public IActionResult Add()
        {
            return View(new AddPlayerFormModel());
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var player = this.data.Players.FirstOrDefault(c => c.Id == id);

            data.Players.Remove(player);

            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All([FromQuery]AllPlayersQueryModel query)
        {
            var queryResult = this.players.All(
                query.SearchTerm,
                query.Gender);

            query.TotalPlayers = queryResult.TotalPlayers;
            query.Players = queryResult.Players;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public IActionResult Add(AddPlayerFormModel player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            // if image is NULL or image name doesn't end to ".jpg"
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
            var uploadsFolder = Path.Combine("wwwroot/UploadedPhotos/ProfilePhotos/");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePhoto.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                profilePhoto.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
