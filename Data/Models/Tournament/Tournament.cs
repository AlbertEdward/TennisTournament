using System.ComponentModel.DataAnnotations.Schema;

namespace TennisTournament.Data.Models
{
    public class Tournament
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public GameType GameType { get; init; }

        public CourtType CourtType { get; init; }

        public Set Sets { get; init; }

        public Game Games { get; init; }

        public Rule Rules { get; init; }

        public LastSet LastSets { get; init; }

        public string Description { get; set; }

        [ForeignKey("Players")]
        public int PlayerId { get; init; }
        public IEnumerable<Player>? Players { get; init; }

        //public int DealerId { get; init; }
        //public Dealer Dealer { get; init; }
    }
}
