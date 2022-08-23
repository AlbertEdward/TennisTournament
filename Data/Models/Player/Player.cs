using System.ComponentModel.DataAnnotations.Schema;

namespace TennisTournament.Data.Models
{
    public class Player
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public StrongHand StrongHand { get; set; }

        public BackHandStroke BackHandStroke { get; set; }

        public double Rank { get; set; } = 10;

        public int Wins { get; set; } = 0;

        public int Losses { get; set; } = 0;

        public int TotalMatches { get; set; } = 0;

        public string ProfilePhoto { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();

        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
