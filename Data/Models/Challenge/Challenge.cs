namespace TennisTournament.Data.Models
{
    public class Challenge
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public CourtType CourtType { get; set; }

        public Set Sets { get; set; }

        public Game Games { get; set; }

        public Rule Rules { get; set; }

        public LastSet LastSets { get; set; }

        public string Description { get; set; }

        public string Winner { get; set; }

        public string Loser { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public string PlayerHostUserId { get; set; }

        public int PlayerGuestId { get; set; }
    }
}
