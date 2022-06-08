using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisTournament.Data.Models
{
    public class Tournament
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int GameTypeId { get; set; }
        public GameType GameType { get; init; }

        public int CourtTypeId { get; set; }
        public CourtType CourtType { get; init; }

        public int SetId { get; set; }
        public Set Sets { get; init; }

        public int GameId { get; set; }
        public Game Games { get; init; }

        public int RuleId { get; set; }
        public Rule Rules { get; init; }

        public int LastSetId { get; set; }
        public LastSet LastSets { get; init; }

        public int PlayerId { get; init; }

        public ICollection<Player> Players { get; init; }
    }
}
