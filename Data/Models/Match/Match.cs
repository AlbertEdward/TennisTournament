using TennisTournament.Data.Models;

namespace TennisTournament.Data
{
    public class Match
    {
        public int id { get; set; }

        public int FirstPlayerId { get; set; }

        public int SecondPlayerId { get; set; }

        public Tournament Tournament { get; set; }
    }
}
