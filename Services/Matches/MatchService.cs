using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Services.Matches.Models;

namespace TennisTournament.Services.Matches
{
    public class MatchService : IMatchService
    {
        private readonly TennisDbContext data;

        public MatchService(TennisDbContext data)
        {
            this.data = data;
        }

        public void CreateMatch(MatchServiceModel match, int id)
        {
            var tournament = data.Tournaments.Include(t => t.Players).FirstOrDefault(t => t.Id == id);

            var random = new Random();
            var randomized = tournament.Players.OrderBy(r => random.Next()).ToList();

            for (int i = 0; i < randomized.Count() - 1; i++)
            {
                var firstPlayerId = randomized[i];
                var secondPlayerId = randomized[i+1];

                var matchData = new Match
                {
                    FirstPlayerId = firstPlayerId.Id,
                    SecondPlayerId = secondPlayerId.Id,
                    TournamentId = id,
                };

                this.data.Matches.Add(matchData);

                randomized.Remove(firstPlayerId);
                randomized.Remove(secondPlayerId);

                i = -1;
            }

            this.data.SaveChanges();
        }
    }
}
