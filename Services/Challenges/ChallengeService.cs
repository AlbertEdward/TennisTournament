using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;

namespace TennisTournament.Services.Challenges
{
    public class ChallengeService : IChallengeService
    {
        private readonly TennisDbContext data;

        public ChallengeService(TennisDbContext data)
        {
            this.data = data;
        }
        public void AddPlayerToChallenge(string playerHostId, int playerGuestId)
        {
            var playerHost = data.Players.FirstOrDefault(p => p.UserId == playerHostId);
            var playerGuest = data.Players.FirstOrDefault(p => p.Id == 1);

            var challenge = data.Challenges.FirstOrDefault(c => c.PlayerHostId == playerHost.UserId);

            challenge.Player.Add(playerHost);
            challenge.Player.Add(playerGuest);

            this.data.SaveChanges();
        }

        public void RemovePlayerFromChallenge(string playerHostId, int playerGuestId)
        {
            var playerHost = data.Players.Include(p => p.Challenges).FirstOrDefault(p => p.UserId == playerHostId);
            var playerGuest = data.Players.Include(p => p.Challenges).FirstOrDefault(p => p.Id == 1);//TODO

            var challenge = data.Challenges.FirstOrDefault(c => c.PlayerHostId == playerHost.UserId);

            challenge.Player.Clear();

            this.data.SaveChanges();
        }
    }
}
