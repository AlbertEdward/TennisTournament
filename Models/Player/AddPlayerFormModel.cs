using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;

namespace TennisTournament.Models.Player
{
    public class AddPlayerFormModel
    {
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; init; }

        [Display(Name = "Strong Hand")]
        public StrongHand StrongHand { get; init; }

        [Display(Name = "Back hand stroke")]
        public BackHandStroke BackHandStroke { get; init; }
    }
}
