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
        public IActionResult Add(AddPlayerFormModel player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            var playerData = new Player
            {
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke
            };

            this.data.Players.Add(playerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
