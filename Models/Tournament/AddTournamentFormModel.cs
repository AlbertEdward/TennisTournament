using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;

namespace TennisTournament.Models.Tournament
{
    public class AddTournamentFormModel
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public GameType GameTypes { get; set; }

        [Required]
        public CourtType CourtTypes { get; set; }

        [Required]
        public Set Sets { get; set; }

        [Required]
        public Game Games { get; set; }

        [Required]
        public Rule Rules { get; set; }

        [Required]
        public LastSet LastSets { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
