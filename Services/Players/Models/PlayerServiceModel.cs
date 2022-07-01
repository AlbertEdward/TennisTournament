using TennisTournament.Data.Models;

namespace TennisTournament.Services.Players
{
    public class PlayerServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public StrongHand StrongHand { get; set; }

        public BackHandStroke BackHandStroke { get; set; }

        public double Rank { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public string ProfilePhoto { get; set; }
    }
}
