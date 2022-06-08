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

        public string Rank { get; init; }

        public int Wins { get; init; }

        public int Losts { get; init; }

        public int TotalMatches { get; init; }

        public int TournamentId { get; init; }

        public Tournament Tournament { get; init; }
    }
}
