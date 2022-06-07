using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisTournament.Data.Models
{
    public class Tournament
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        public int TypeId { get; set; }
        public GameType GameType { get; init; }

        public int CourtTypeId { get; set; }
        public CourtType CourtType { get; init; }

        public int SetId { get; set; }
        public Set Sets { get; init; }

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
