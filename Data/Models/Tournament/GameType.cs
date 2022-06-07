namespace TennisTournament.Data.Models
{
    public class GameType
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
