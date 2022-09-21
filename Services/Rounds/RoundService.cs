using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Services.Rounds.Models;

namespace TennisTournament.Services.Rounds
{
    public class RoundService : IRoundService
    {
        private readonly TennisDbContext data;

        public RoundService(TennisDbContext data)
        {
            this.data = data;
        }

        public void CreateRound(RoundServiceModel round, int tournamentId)
        {
            var tournament = this.data.Tournaments.Include(p => p.Players).FirstOrDefault(t => t.Id == tournamentId);

            var playerCount = tournament.Players.Count();

            
        }
    }
}
