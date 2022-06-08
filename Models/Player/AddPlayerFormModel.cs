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

        public StrongHand StrongHand { get; init; }

        public BackHandStroke BackHandStroke { get; init; }
    }
}
