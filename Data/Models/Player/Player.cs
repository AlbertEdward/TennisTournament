using System.ComponentModel.DataAnnotations.Schema;

namespace TennisTournament.Data.Models
{
    public class Player
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Age { get; init; }

        public Gender Gender { get; init; }

        public StrongHand StrongHand { get; init; }

        public BackHandStroke BackHandStroke { get; init; }

        public double Rank { get; init; } = 0.00;

        public int Wons { get; init; } = 0;

        public int Losts { get; init; } = 0;

        public int TotalMatches { get; init; } = 0;

        public string ProfilePhoto { get; init; }

        [ForeignKey("Tournament")]
        public int TournamentId { get; init; }
        public IEnumerable<Tournament> Tournaments { get; init; }
    }
}
