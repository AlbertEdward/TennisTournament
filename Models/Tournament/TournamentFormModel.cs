using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;

namespace TennisTournament.Models.Tournament
{
    public class TournamentFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be in range 3-60 characters!")]
        public string Name { get; init; }

        public double? MinRank { get; set; }

        [Required]
        public GameType GameType { get; set; }

        [Required]
        public CourtType CourtType { get; set; }

        [Required]
        public Set Set { get; set; }

        [Required]
        public Game Game { get; set; }

        [Required]
        public Rule Rule { get; set; }

        [Required]
        public LastSet LastSet { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Description must be in range 10-255 characters!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose photo to upload.")]
        [Display(Name = "Upload photo")]
        public IFormFile CoverPhoto { get; set; }
    }
}
