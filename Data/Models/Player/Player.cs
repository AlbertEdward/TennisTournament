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

        public double Rank { get; init; } = 10;

        public int Wons { get; init; } = 0;

        public int Losts { get; init; } = 0;

        public int TotalMatches { get; init; } = 0;

        public string ProfilePhoto { get; set; }

        public string UserId { get; set; }

        public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();

        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
