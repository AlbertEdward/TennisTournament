namespace TennisTournament.Data.Models
{
    public class Tournament
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public double? MinRank { get; set; }

        public TournamentType TournamentType { get; set; }

        public GameType GameType { get; set; }

        public CourtType CourtType { get; set; }

        public Set Set { get; set; }

        public Game Game { get; set; }

        public Rule Rule { get; set; }

        public LastSet LastSet { get; set; }

        public DateTime StartDate { get; set; }

        public string Description { get; set; }

        public string CoverPhoto { get; set; }

        public ICollection<Match> Match { get; set; } = new List<Match>();

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
