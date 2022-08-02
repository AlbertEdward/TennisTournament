using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Models.Challenge;

namespace TennisTournament.Services.Challenges
{
    public class ChallengeService : IChallengeService
    {
        private readonly TennisDbContext data;

        public ChallengeService(TennisDbContext data)
        {
            this.data = data;
        }

        public void AddPlayerToChallenge(string playerHostUserId, int id)
        {
            var playerHost = data.Players.FirstOrDefault(p => p.UserId == playerHostUserId);
            var playerGuest = data.Players.FirstOrDefault(p => p.Id == id);

            var challenge = data.Challenges
                .FirstOrDefault(c => c.PlayerHostUserId == playerHostUserId || c.PlayerGuestId == id);

            if (challenge == null)
            {
            }

            if (challenge.Players.Count == 0)
            {
                challenge.Players.Add(playerHost);
                challenge.Players.Add(playerGuest);
            }

            this.data.SaveChanges();
        }

        public void RemovePlayerFromChallenge(string playerHostUserId, int id)
        {
            var challenge = data.Challenges
                .Include(c => c.Players)
                .FirstOrDefault(c => c.PlayerHostUserId == playerHostUserId || c.PlayerGuestId == id);

            challenge.Players.Clear();

            this.data.SaveChanges();
        }

        public AllChallengesQueryModel Details(int Id)
        => this.data
            .Challenges
            .Where(c => c.Id == Id)
            .Select(challenges => new AllChallengesQueryModel
            {
                Id = challenges.Id,
                Name = challenges.Name,
                CourtTypes = challenges.CourtType,
                Sets = challenges.Sets,
                Games = challenges.Games,
                Rules = challenges.Rules,
                LastSets = challenges.LastSets,
                Description = challenges.Description
            })
            .FirstOrDefault();
    }
}
