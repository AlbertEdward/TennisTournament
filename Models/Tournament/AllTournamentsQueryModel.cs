using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;

namespace TennisTournament.Models.Tournament
{
    public class AllTournamentsQueryModel
    {
        public IEnumerable<TournamentListingViewModel> Tournaments { get; init; }

        public string Name { get; init; }

        [Display(Name = "Game type:")]
        public GameType GameType { get; init; }

        [Display(Name = "Court type:")]
        public CourtType CourtTypes { get; init; }

        [Display(Name = "Search:")]
        public string SearchTerm { get; init; }

        [Display(Name = "Image:")]
        public string CoverPhoto { get; init; }
    }
}