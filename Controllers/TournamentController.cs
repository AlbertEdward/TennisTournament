using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;


namespace TennisTournament.Controllers
{
    public class TournamentController : Controller
    {
        private readonly TennisDbContext data;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TournamentController(TennisDbContext data, IWebHostEnvironment webHostEnvironment)
        {
            this.data = data;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new AddTournamentFormModel());
        }

        public IActionResult Upload()
        {
            return RedirectToAction(nameof(All));
        }

        public IActionResult All(CourtType courtType, GameType gameType, string searchTerm)
        {
            var tournamentsQuery = this.data.Tournaments.AsQueryable();

            if (courtType != CourtType.Select)
            {
                tournamentsQuery = tournamentsQuery.Where(c => c.CourtType == courtType);
            }

            if (gameType != GameType.Select)
            {
                tournamentsQuery = tournamentsQuery.Where(g => g.GameType == gameType);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                tournamentsQuery = tournamentsQuery.Where(t =>
                    t.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    t.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            var tournaments = tournamentsQuery
                .OrderByDescending(t => t.Id)
                .Select(t => new TournamentListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    GameType = t.GameType,
                    CourtType = t.CourtType,
                })
                .ToList();

            return View(new AllTournamentsQueryModel
            {
                Tournaments = tournaments,
                CourtTypes = courtType,
                GameType = gameType,
                SearchTerm = searchTerm,
            });
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public IActionResult Add(AddTournamentFormModel tournament, IFormFile coverImage)
        {
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }

            if (coverImage!= null)
            {
                if (coverImage.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = coverImage.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    tournament.CoverImage = p1;
                }
            }

            var tournamentData = new Tournament
            {
                Name = tournament.Name,
                GameType = tournament.GameTypes,
                CourtType = tournament.CourtTypes,
                Sets = tournament.Sets,
                Games = tournament.Games,
                Rules = tournament.Rules,
                LastSets = tournament.LastSets,
                Description = tournament.Description
            };

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
