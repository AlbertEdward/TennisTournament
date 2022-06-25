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

        public double Rank { get; init; } = 0.00;

        public int Wons { get; init; } = 0;

        public int Losts { get; init; } = 0;

        public int TotalMatches { get; init; } = 0;

        public string ProfilePhoto { get; set; }

        [ForeignKey("Tournament")]
        public int TournamentId { get; init; }
        public IEnumerable<Tournament> Tournaments { get; set; }
    }
}
