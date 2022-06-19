using TennisTournament.Data;

namespace TennisTournament.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly TennisDbContext data;

        public StatisticsService(TennisDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalTournaments = this.data.Tournaments.Count();
            var totalPlayers = this.data.Players.Count();

            return new StatisticsServiceModel
            {
                TotalPlayers = totalPlayers,
                TotalTournaments = totalTournaments,
            };
        }
    }
}
