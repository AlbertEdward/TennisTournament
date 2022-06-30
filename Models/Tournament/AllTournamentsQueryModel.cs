using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;
using TennisTournament.Services.Tournaments;

namespace TennisTournament.Models.Tournament
{
    public class AllTournamentsQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Game type:")]
        public GameType GameType { get; set; }

        [Display(Name = "Court type:")]
        public CourtType CourtType { get; set; }

        [Display(Name = "Search:")]
        public string SearchTerm { get; set; }

        [Display(Name = "Image:")]
        public string CoverPhoto { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        public int TotalTournaments { get; set; }

        public IEnumerable<TournamentServiceModel> Tournaments { get; set; }

        public int PlayerId { get; set; }
    }
}