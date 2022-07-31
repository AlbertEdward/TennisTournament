namespace TennisTournament.Data.Models.PlayerTournament
{
    public class PlayerTournament
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }


        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
    }
}
