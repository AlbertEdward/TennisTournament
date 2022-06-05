using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Data.Models
{
    public class Tournament
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int TypeId { get; set; }

        public TypeOfGame Type { get; init; }

        public string MatchType { get; init; }

        public string Sets { get; init; }

        public string Games { get; init; }

        public string Rule { get; init; }

        public string LastSet { get; init; }

        public int PlayerId { get; init; }

        public ICollection<Player> Players { get; init; }
    }
}
