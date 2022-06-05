using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Models.Tournament
{
    public class AddTournamentFormModel
    {
        public string Name { get; init; }

        [Display(Name = "Type")]
        public int TypeId { get; init; }

        public IEnumerable<TypeOfGameViewModel> TypeOfGames { get; set; }

        public string MatchType { get; init; }

        public string Sets { get; init; }

        public string Games { get; init; }

        public string Rule { get; init; }

        public string LastSet { get; init; }
    }
}
