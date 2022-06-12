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
        public IActionResult Add() => View();

        [Authorize]
        public IActionResult All()
        {
            var players = this.data
                .Players
                .OrderByDescending(p => p.Id)
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    Rank = p.Rank
                })
                .ToList();

            return View(players);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddPlayerFormModel player, IFormFile image)
        {
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
