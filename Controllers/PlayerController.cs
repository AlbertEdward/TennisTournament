using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Player;

namespace TennisTournament.Controllers
{
    public class PlayerController : Controller
    {
        private readonly TennisDbContext data;

        public PlayerController(TennisDbContext data)
            => this.data = data;

        [Authorize] 
        public IActionResult Add()
        {
            return View(new AddPlayerFormModel());
        }

        [Authorize]
        public IActionResult All(string searchTerm, Gender gender)
        {
            var playersQuery = this.data.Players.AsQueryable();

            if (gender != Gender.Select)
            {
                playersQuery = playersQuery.Where(c => c.Gender == gender);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                playersQuery = playersQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var players = playersQuery
                .OrderByDescending(p => p.Id)
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    Rank = p.Rank,
                    ProfilePhoto = p.ProfilePhoto
                })
                .ToList();

            return View(new AllPlayersQueryModel
            {
                Players = players,
                Name = searchTerm,
                Gender = gender
            });
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

            // if image is NULL or image name doesn't end to ".jpg" or ".jpeg"
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
            var uploadsFolder = Path.Combine("C:/Users/Albert Khurshudyan/Desktop/TennisTournament/wwwroot/UploadedPhotos/ProfilePhotos/");
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
