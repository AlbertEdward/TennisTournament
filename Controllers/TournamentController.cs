﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;
using TennisTournament.Services;
using TennisTournament.Services.Tournaments;

namespace TennisTournament.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IUploadFileService uploadFileService;
        private readonly ITournamentService tournamentService;
        private readonly TennisDbContext data;

        public TournamentController(TennisDbContext data, ITournamentService tournaments, IUploadFileService uploadFileService)
        {
            this.data = data;
            this.tournamentService = tournaments;
            this.uploadFileService = uploadFileService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new TournamentFormModel());
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var tournament = this.tournamentService.Details(id);

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
        public IActionResult Edit(int id, TournamentFormModel tournament)
        {
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }

            var tournamentIsEdited = this.tournamentService.Edit(
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

        public IActionResult Details(int id)
        {
            var tournament = this.tournamentService.Details(id);

            return View(new TournamentFormModel
            {
                Id = id,
                Name = tournament.Name,
                CourtTypes = tournament.CourtType,
                GameTypes = tournament.GameType,
                Games = tournament.Game,
                Sets = tournament.Set,
                Rules = tournament.Rule,
                LastSets = tournament.LastSet,
                Description = tournament.Description
            });
        }

        public IActionResult Delete(int id)
        {
            var deleted = this.tournamentService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery]AllTournamentsQueryModel query)
        {
            var queryResult = this.tournamentService.All(
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
        public IActionResult Add(TournamentFormModel tournament)
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

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private string UploadCoverPhoto(IFormFile coverPhoto)
        {
            var photo = this.uploadFileService.CoverPhoto(coverPhoto);

            return photo;
        }
    }
}
