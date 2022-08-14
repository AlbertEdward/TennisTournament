using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Bracket(int id)
        {
            var bracket = await this.tournamentService.DetailsAsync(id);

            return View(new TournamentServiceModel
            {
                Id = id,
                Name = bracket.Name,
                MinRank = bracket.MinRank,
                CourtType = bracket.CourtType,
                GameType = bracket.GameType,
                Game = bracket.Game,
                Set = bracket.Set,
                Rule = bracket.Rule,
                LastSet = bracket.LastSet,
                Description = bracket.Description,
                CoverPhoto = bracket.CoverPhoto,
                Players = bracket.Players,
                Matches = bracket.Matches
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddPlayerToTournament(int tournamentId)
        {
            if (!UserIsPlayer())
            {
                return RedirectToAction("AddPlayer", "Player");
            }

            this.tournamentService.AddPlayerToTournament(this.User.GetId(), tournamentId);

            return RedirectToAction("All", "Player");
        }

        [HttpGet]
        [Authorize]
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
                MinRank = tournament.MinRank,
                GameType = tournament.GameType,
                CourtType = tournament.CourtType,
                Set = tournament.Set,
                Game = tournament.Game,
                Rule = tournament.Rule,
                LastSet = tournament.LastSet,
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


            string coverPhoto = this.UploadCoverPhoto(tournament.CoverPhoto);

            var tournamentIsEdited = await this.tournamentService.EditAsync(
                id,
                tournament.Name,
                tournament.MinRank,
                tournament.CourtType,
                tournament.GameType,
                tournament.Set,
                tournament.Game,
                tournament.Rule,
                tournament.LastSet,
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

            return View(new TournamentServiceModel
            {
                Id = id,
                Name = tournament.Name,
                MinRank = tournament.MinRank,
                CourtType = tournament.CourtType,
                GameType = tournament.GameType,
                Game = tournament.Game,
                Set = tournament.Set,
                Rule = tournament.Rule,
                LastSet = tournament.LastSet,
                Description = tournament.Description,
                CoverPhoto = tournament.CoverPhoto,
                Players = tournament.Players,
                Matches = tournament.Matches
            });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await this.tournamentService.DeleteAsync(id);

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
        public IActionResult AddTournament()
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
                MinRank = tournament.MinRank,
                GameType = tournament.GameType,
                CourtType = tournament.CourtType,
                Set = tournament.Set,
                Game = tournament.Game,
                Rule = tournament.Rule,
                LastSet = tournament.LastSet,
                Description = tournament.Description,
                CoverPhoto = coverPhoto
            };

            this.tournamentService.CreateTournament(tournament, coverPhoto);

            return RedirectToAction(nameof(All));
        }

        public bool UserIsPlayer()
        {
            var userId = this.User.GetId();

            return this.playerService.UserIsPlayer(userId);
        }

        private string UploadCoverPhoto(IFormFile coverPhoto)
        {
            var photo = this.uploadFileService.CoverPhoto(coverPhoto);

            return photo;
        }
    }
}