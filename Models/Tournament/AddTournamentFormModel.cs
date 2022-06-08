using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Models.Tournament
{
    public class AddTournamentFormModel
    {
        [Required]
        public string Name { get; init; }

        [Display(Name = "Type: ")]
        public int GameTypeId { get; init; }
        public IEnumerable<ViewModel>? GameTypes { get; set; }

        [Display(Name = "Court: ")]
        public int CourtTypeId { get; init; }
        public IEnumerable<ViewModel>? CourtTypes { get; set; }

        [Display(Name = "Sets: ")]
        public int SetId { get; init; }
        public IEnumerable<ViewModel>? Sets { get; set; }

        [Display(Name = "Games per set: ")]
        public int GameId { get; init; }
        public IEnumerable<ViewModel>? Games { get; set; }

        [Display(Name = "Rule: ")]
        public int RuleId { get; init; }
        public IEnumerable<ViewModel>? Rules { get; set; }

        [Display(Name = "Last Set: ")]
        public int LastSetId { get; init; }
        public IEnumerable<ViewModel>? LastSets { get; set; }
    }
}
