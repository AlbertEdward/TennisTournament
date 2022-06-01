using Microsoft.AspNetCore.Mvc;
using TennisTournament.Models.Player;

namespace TennisTournament.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddPlayerFormModel player)
        {
            return View();
        }
    }
}
