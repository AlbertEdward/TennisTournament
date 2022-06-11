using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Data.Models
{
    public class Dealer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string UserId { get; init; }

        public IEnumerable<Tournament> Tournaments { get; init; } = new List<Tournament>();
    }
}
