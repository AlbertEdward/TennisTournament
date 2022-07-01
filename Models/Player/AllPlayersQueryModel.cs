using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;
using TennisTournament.Services.Players;

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

        public IEnumerable<PlayerServiceModel> Players { get; set; }
    }
}
