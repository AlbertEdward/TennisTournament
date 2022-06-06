using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Data.Models
{
    public class Tournament
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        public int TypeId { get; set; }

        public TypeOfGame Type { get; init; }

        [Required]
        public string MatchType { get; init; }

        [Required]
        public string Sets { get; init; }

        [Required]
        public string Games { get; init; }

        [Required]
        public string Rule { get; init; }

        [Required]
        public string LastSet { get; init; }

        public int PlayerId { get; init; }

        public ICollection<Player> Players { get; init; }
    }
}
