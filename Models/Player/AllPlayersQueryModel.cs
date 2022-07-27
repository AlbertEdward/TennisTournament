using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;
using TennisTournament.Services.Players;
using TennisTournament.Services.Tournaments;

namespace TennisTournament.Models.Player
{
    public class AllPlayersQueryModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public int Age { get; set; }

        public Gender Gender { get; init; }

        public StrongHand StrongHand { get; init; }

        public BackHandStroke BackHandStroke { get; init; }

        [Display(Name = "Search:")]
        public string SearchTerm { get; init; }

        [Display(Name = "Photo:")]
        public string ProfilePhoto { get; init; }

        public int TotalPlayers { get; set; }

        public double Rank { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public ICollection<PlayerServiceModel> Players { get; set; }

        public ICollection<TournamentServiceModel> Tournaments { get; set; }
    }
}
