using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;

namespace TennisTournament.Models.Player
{
    public class AddPlayerFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; init; }

        [Required]
        [Display(Name = "Strong Hand")]
        public StrongHand StrongHand { get; init; }

        [Required]
        [Display(Name = "Back hand stroke")]
        public BackHandStroke BackHandStroke { get; init; }
    }
}
