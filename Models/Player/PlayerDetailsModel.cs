using TennisTournament.Data.Models;

namespace TennisTournament.Models.Player
{
    public class PlayerDetailsModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public int Age { get; set; }

        public StrongHand StrongHand { get; init; }

        public BackHandStroke BackHandStroke { get; init; }

        public Gender Gender { get; init; }

        public string SearchTerm { get; init; }

        public int TotalPlayers { get; set; }

        public IFormFile ProfilePhoto { get; set; }

        public double Rank { get; set; } = 0.00;

        public int MyProperty { get; set; }
    }
}
