using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Models.Tournament
{
    public class AddTournamentFormModel
    {
        public string Name { get; init; }

        [Display(Name = "Type: ")]
        public int TypeId { get; init; }
        public IEnumerable<ViewModel> GameTypes { get; set; }

        [Display(Name = "Court: ")]
        public int CourtTypeId { get; init; }
        public IEnumerable<ViewModel> CourtTypes { get; set; }

        public int SetId { get; init; }
        public IEnumerable<ViewModel> Sets { get; set; }

        public string Games { get; init; }

        public string Rule { get; init; }

        [Display(Name = "Last Set: ")]
        public string LastSet { get; init; }
    }
}
