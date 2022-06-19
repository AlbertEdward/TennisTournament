using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Api;
using TennisTournament.Models.Tournament;
using TennisTournament.Services.Tournaments;

namespace TennisTournament.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournaments;
        private readonly TennisDbContext data;

        public TournamentController(ITournamentService tournaments, TennisDbContext data)
        {
            this.data = data;
            this.tournaments = tournaments;
        }


        [Authorize]
        public IActionResult Add()
        {
            return View(new AddTournamentFormModel());
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var tournament = this.data.Tournaments.FirstOrDefault(c => c.Id == id);

            data.Tournaments.Remove(tournament);

            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All([FromQuery]AllTournamentsQueryModel query)
        {
            var queryResult = this.tournaments.All(
                query.Name,
                query.SearchTerm,
                query.CourtType,
                query.GameType);

            query.TotalTournaments = queryResult.TotalTournaments;
            query.Tournaments = queryResult.Tournaments;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public IActionResult Add(AddTournamentFormModel tournament)
        {
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }

            // if image is NULL or image name doesn't end to ".jpg"
            if (tournament.CoverPhoto == null || !(tournament.CoverPhoto.FileName.EndsWith(".jpg")))
            {
                return View(tournament);
            }

            string coverPhoto = this.UploadFile(tournament.CoverPhoto);

            var tournamentData = new Tournament
            {
                Name = tournament.Name,
                GameType = tournament.GameTypes,
                CourtType = tournament.CourtTypes,
                Sets = tournament.Sets,
                Games = tournament.Games,
                Rules = tournament.Rules,
                LastSets = tournament.LastSets,
                Description = tournament.Description,
                CoverPhoto = coverPhoto
            };

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private string UploadFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine("wwwroot/UploadedPhotos/CoverPhotos/");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
