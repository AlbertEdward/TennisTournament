using System.ComponentModel.DataAnnotations;
using TennisTournament.Data.Models;
using TennisTournament.Services.Challenges.Models;
using TennisTournament.Services.Players;

namespace TennisTournament.Models.Challenge
{
    public class AllChallengesQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Court type:")]
        public CourtType CourtTypes { get; set; }

        public Set Sets { get; set; }

        public Game Games { get; set; }

        public Rule Rules { get; set; }

        public LastSet LastSets { get; set; }

        public string Description { get; set; }

        public string HostUserId { get; set; }

        public int GuestId { get; set; }

        public ICollection<ChallengeServiceModel> Players { get; set; }

        public ICollection<ChallengeServiceModel> Challenges { get; set; }

    }
}