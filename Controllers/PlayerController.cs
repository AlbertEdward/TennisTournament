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

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddPlayerFormModel player)
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

            return RedirectToAction("Index", "Home");
        }
    }
}
