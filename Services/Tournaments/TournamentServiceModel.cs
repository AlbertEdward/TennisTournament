using TennisTournament.Data.Models;

namespace TennisTournament.Services.Tournaments
{
    public class TournamentServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public GameType GameType { get; set; }

        public CourtType CourtType { get; set; }

        public string Description { get; set; }

        public string CoverPhoto { get; set; }
    }
}
