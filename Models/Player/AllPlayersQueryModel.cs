using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;

namespace TennisTournament.Models.Player
{
    public class AllPlayersQueryModel
    {
        public IEnumerable<PlayerListingViewModel> Players { get; init; }

        public string Name { get; init; }

        public Gender Gender { get; init; }

        [Display(Name = "Search:")]
        public string SearchTerm { get; init; }

        [Display(Name = "Photo:")]
        public string ProfilePhoto { get; init; }
    }
}
