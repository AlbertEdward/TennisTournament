namespace TennisTournament.Data.Models
{
    public class Challenge
    {
        public int Id { get; set; }

        public ICollection<Player> Player { get; set; } = new List<Player>();
    }
}
