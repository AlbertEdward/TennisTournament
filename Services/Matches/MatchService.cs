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

        public void CreateMatch(MatchServiceModel match, int tournamentId)
        {
            var tournament = data.Tournaments.Include(t => t.Players).FirstOrDefault(t => t.Id == tournamentId);

            var random = new Random();
            var randomized = tournament.Players.OrderBy(r => random.Next()).ToList();

            // remove 1 player if uneven players
            // TODO: handle case
            if (randomized.Count() %2 != 0) {
                // create 1 more game;
                
                var playerToRemove = randomized[randomized.Count() -1];
                randomized.Remove(playerToRemove);
            }

            for (int i = 0; i < randomized.Count(); i+=2)
            {
                var firstPlayerId = randomized[i];
                var secondPlayerId = randomized[i+1];

                var matchData = new Match
                {
                    Id = match.Id,
                    FirstPlayerId = firstPlayerId.Id,
                    SecondPlayerId = secondPlayerId.Id,
                    TournamentId = tournamentId,
                };

                this.data.Matches.Add(matchData);
            }

            this.data.SaveChanges();
        }

        public MatchServiceModel Details(int Id)
        => this.data
            .Matches
            .Where(m => m.Id == Id)
            .Select(matches => new MatchServiceModel
            {
                Id = matches.Id,
                FirstPlayerId = matches.FirstPlayerId,
                SecondPlayerId = matches.SecondPlayerId,
                TournamentId = matches.TournamentId
            })
            .FirstOrDefault();

        public void MatchResult(int matchId, int winnerId)
        {
            var match = this.data.Matches.FirstOrDefault(c => c.Id == matchId);

            match.Winner = winnerId;

            if (match.FirstPlayerId != winnerId)
            {
                match.Loser = match.FirstPlayerId;
            }
            else
            {
                match.Loser = match.SecondPlayerId;
            }

            var winner = data.Players.FirstOrDefault(p => p.Id == winnerId);
            var loser = data.Players.FirstOrDefault(p => p.Id == match.Loser);

            winner.Wins += 1;
            loser.Losses += 1;

            winner.TotalMatches += 1;
            loser.TotalMatches += 1;

            winner.Rank = 10.00 - (winner.Wins + winner.Losses) / winner.TotalMatches;
            loser.Rank = 10.00 + (loser.Wins + loser.Losses) / loser.TotalMatches;

            if (winner.Rank > 10)
            {
                winner.Rank = 10.00;
            }
            else if (loser.Rank > 10)
            {
                loser.Rank = 10.00;
            }

            this.data.SaveChanges();
        }
    }
}
