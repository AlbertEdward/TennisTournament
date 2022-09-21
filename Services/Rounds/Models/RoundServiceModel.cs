using TennisTournament.Data.Models;

namespace TennisTournament.Services.Rounds.Models
{
    public class RoundServiceModel
    {
        public int id { get; init; }

        public ICollection<Match> Matches { get; set; } = new List<Match>();

        public int TournamentId { get; init; }
    }
}
