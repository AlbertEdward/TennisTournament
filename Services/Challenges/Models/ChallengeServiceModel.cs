using TennisTournament.Data.Models;

namespace TennisTournament.Services.Challenges.Models
{
    public class ChallengeServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CourtType CourtType { get; set; }

        public Set Set { get; set; }

        public Game Game { get; set; }

        public Rule Rule { get; set; }

        public LastSet LastSet { get; set; }

        public string Description { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
