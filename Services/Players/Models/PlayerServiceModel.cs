using TennisTournament.Data.Models;

namespace TennisTournament.Services.Players
{
    public class PlayerServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; init; }

        public StrongHand StrongHand { get; init; }

        public BackHandStroke BackHandStroke { get; init; }

        public double Rank { get; init; }

        public string ProfilePhoto { get; set; }
    }
}
