using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Models.Dealer
{
    public class DealerFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
