using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Models.Match
{
    public class MatchWinnerFormModel
    {
        [Required]
        public int WinnerId { get; set; }

        [Required]
        public int LoserId { get; set; }
    }
}
