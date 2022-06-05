using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TennisTournament.Models;

namespace TennisTournament.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}