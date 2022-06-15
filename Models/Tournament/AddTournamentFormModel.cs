using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TennisTournament.Data.Models;

namespace TennisTournament.Models.Tournament
{
    public class AddTournamentFormModel
    {
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be in range 3-60 characters!")]
        public string Name { get; init; }

        [Required]
        public GameType GameTypes { get; set; }

        [Required]
        public CourtType CourtTypes { get; set; }

        [Required]
        public Set Sets { get; set; }

        [Required]
        public Game Games { get; set; }

        [Required]
        public Rule Rules { get; set; }

        [Required]
        public LastSet LastSets { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Description must be in range 10-255 characters!")]
        public string Description { get; set; }

        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public byte[] CoverImage { get; set; }
    }
}
