using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Models.Tournament
{
    public class AddTournamentFormModel
    {
        public string Name { get; init; }

        [Display(Name = "Type: ")]
        public int TypeId { get; init; }
        public IEnumerable<TypeOfGameViewModel> TypeOfGames { get; set; }

        [Display(Name = "Match Type: ")]
        public int MatchTypeId { get; init; }
        public IEnumerable<MatchType> MatchTypes { get; set; }


        public string Sets { get; init; }

        public string Games { get; init; }

        public string Rule { get; init; }

        [Display(Name = "Last Set: ")]
        public string LastSet { get; init; }
    }
}
