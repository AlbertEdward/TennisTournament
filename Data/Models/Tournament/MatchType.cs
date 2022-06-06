namespace TennisTournament.Data.Models
{
    public class MatchType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
