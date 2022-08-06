using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Infrastructure;
using TennisTournament.Models.Tournament;
using TennisTournament.Services;
using TennisTournament.Services.Players;
using TennisTournament.Services.Tournaments;

namespace TennisTournament.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;
        private readonly IPlayerService playerService;
        private readonly IUploadFileService uploadFileService;

        public TournamentController(
            ITournamentService tournaments,
            IPlayerService playerService,
            IUploadFileService uploadFile)
        {
            this.tournamentService = tournaments;
            this.playerService = playerService;
            this.uploadFileService = uploadFile;
        }

        [HttpGet]
        public IActionResult AddPlayerToTournament(int tournamentId)
        {
            //TODO Error message if try to join second time
            this.tournamentService.AddPlayerToTournament(this.User.GetId(), tournamentId);

            return RedirectToAction("All", "Player");
        }

        [HttpGet]
        public IActionResult RemovePlayerFromTournament(int tournamentId)
        {
            this.tournamentService.RemovePlayerFromTournament(this.User.GetId(), tournamentId);

            return RedirectToAction("All", "Player");

        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var tournament = await this.tournamentService.DetailsAsync(id);

            return View(new TournamentFormModel
            {
                Name = tournament.Name,
                GameTypes = tournament.GameType,
                CourtTypes = tournament.CourtType,
                Sets = tournament.Set,
                Games = tournament.Game,
                Rules = tournament.Rule,
                LastSets = tournament.LastSet,
                Description = tournament.Description
            });
        }

        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> Edit(int id, TournamentFormModel tournament)
        {
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }

            var tournamentIsEdited = await this.tournamentService.EditAsync(
                id,
                tournament.Name,
                tournament.CourtTypes,
                tournament.GameTypes,
                tournament.Sets,
                tournament.Games,
                tournament.Rules,
                tournament.LastSets,
                tournament.Description);

            if (!tournamentIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Details(int id)
        {
            var tournament = await this.tournamentService.DetailsAsync(id);

            return View(new AllTournamentsQueryModel
            {
                Id = id,
                Name = tournament.Name,
                CourtType = tournament.CourtType,
                GameType = tournament.GameType,
                Game = tournament.Game,
                Set = tournament.Set,
                Rule = tournament.Rule,
                LastSet = tournament.LastSet,
                Description = tournament.Description,
                CoverPhoto = tournament.CoverPhoto

            });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = this.tournamentService.DeleteAsync(id);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All([FromQuery]AllTournamentsQueryModel query)
        {
            var queryResult = await this.tournamentService.AllAsync(
                query.Name,
                query.SearchTerm,
                query.CourtType,
                query.GameType);

            query.TotalTournaments = queryResult.TotalTournaments;
            query.Tournaments = queryResult.Tournaments;

            return View(query);
        }

        [Authorize]
        public async Task<IActionResult> AddTournament()
        {
            return View(new TournamentFormModel());
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public IActionResult AddTournament(TournamentFormModel tournament)
        {
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }

            if (tournament.CoverPhoto == null || !(tournament.CoverPhoto.FileName.EndsWith(".jpg")))
            {
                return View(tournament);
            }

            string coverPhoto = this.UploadCoverPhoto(tournament.CoverPhoto);

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

            this.tournamentService.CreateTournament(tournament, coverPhoto);

            return RedirectToAction(nameof(All));
        }

        private string UploadCoverPhoto(IFormFile coverPhoto)
        {
            var photo = this.uploadFileService.CoverPhoto(coverPhoto);

            return photo;
        }
    }
}