using TennisTournament.Data.Models;

namespace TennisTournament.Models.Player
{
    public class PlayerListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; init; }

        public double Rank { get; init; }
    }
}
