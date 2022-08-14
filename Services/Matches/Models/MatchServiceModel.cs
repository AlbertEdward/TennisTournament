namespace TennisTournament.Services.Matches.Models
{
    public class MatchServiceModel
    {
        public int Id { get; set; }

        public int FirstPlayerId { get; set; }

        public int SecondPlayerId { get; set; }

        public int TournamentId { get; set; }
    }
}
