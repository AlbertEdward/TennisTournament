namespace TennisTournament.Data.Models
{
    public class Round
    {
        public int Id { get; set; }

        public ICollection<Match> Matches { get; set; }

        public Tournament Tournament { get; set; }
    }
}
