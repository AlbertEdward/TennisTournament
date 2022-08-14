using TennisTournament.Data.Models;

namespace TennisTournament.Services.Tournaments
{
    public class TournamentServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? MinRank { get; set; }

        public GameType GameType { get; set; }

        public CourtType CourtType { get; set; }

        public Set Set { get; set; }

        public Game Game { get; set; }

        public Rule Rule { get; set; }

        public LastSet LastSet { get; set; }

        public string Description { get; set; }

        public string CoverPhoto { get; set; }

        public ICollection<Player> Players { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
