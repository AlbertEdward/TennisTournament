using TennisTournament.Data.Models;

namespace TennisTournament.Data.Models
{
    public class Match
    {
        public int Id { get; set; }

        public int FirstPlayerId { get; set; }

        public int SecondPlayerId { get; set; }

        public int TournamentId { get; set; }

        public int? Winner { get; set; }

        public int? Loser { get; set; }

        public Tournament Tournament { get; set; }
    }
}
