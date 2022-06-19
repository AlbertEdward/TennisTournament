using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;
using TennisTournament.Services.Tournaments;

namespace TennisTournament.Models.Tournament
{
    public class AllTournamentsQueryModel
    {
        public string Name { get; init; }

        [Display(Name = "Game type:")]
        public GameType GameType { get; init; }

        [Display(Name = "Court type:")]
        public CourtType CourtType { get; init; }

        [Display(Name = "Search:")]
        public string SearchTerm { get; init; }

        [Display(Name = "Image:")]
        public string CoverPhoto { get; init; }

        public int TotalTournaments { get; set; }

        public IEnumerable<TournamentServiceModel> Tournaments { get; set; }
    }
}