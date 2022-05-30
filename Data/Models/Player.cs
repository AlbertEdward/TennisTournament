using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Data.Models
{
    public class Player
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Gender { get; init; }

        [Required]
        public string StrongHand { get; init; }

        public string BackHandStroke { get; init; }


        public string Rank { get; init; }

        public string Wins { get; init; }

        public string Losts { get; init; }

        public string TotalMatches { get; init; }

        public ICollection<Tournament> Tournaments { get; init; }




    }
}
