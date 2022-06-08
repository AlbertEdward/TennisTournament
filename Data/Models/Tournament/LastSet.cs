namespace TennisTournament.Data.Models
{
    public class LastSet
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Tournament> Tournaments { get; init; } = new List<Tournament>();
    }
}
