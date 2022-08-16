using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Models.Challenge
{
    public class ChallengeWinnerFormModel
    {
        [Required]
        public int WinnerId { get; set; }
    }
}
