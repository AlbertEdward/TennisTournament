using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisTournament.Data.Models
{
    public class Tournament
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public double? MinRank { get; set; }

        public GameType GameType { get; set; }

        public CourtType CourtType { get; set; }

        public Set Sets { get; set; }

        public Game Games { get; set; }

        public Rule Rules { get; set; }

        public LastSet LastSets { get; set; }

        public string Description { get; set; }

        public string CoverPhoto { get; set; }

        public ICollection<Player> Player { get; set; } = new List<Player>();
    }
}
