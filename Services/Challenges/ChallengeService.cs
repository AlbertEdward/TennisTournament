using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Challenge;
using TennisTournament.Infrastructure;
using System.Security.Claims;

namespace TennisTournament.Services.Challenges
{
    public class ChallengeService : IChallengeService
    {
        private readonly TennisDbContext data;

        public ChallengeService(TennisDbContext data)
        {
            this.data = data;
        }

        public void AddPlayerToChallenge(string hostUserId, int guestId, int challengeId)
        {
            var playerHost = data.Players.FirstOrDefault(p => p.UserId == hostUserId);
            var playerGuest = data.Players.FirstOrDefault(p => p.Id == guestId);

            var challenge = data.Challenges.FirstOrDefault(c => c.Id == challengeId);

            if (challenge.Players.Count == 0)
            {
                challenge.Players.Add(playerHost);
                challenge.Players.Add(playerGuest);
            }

            this.data.SaveChanges();
        }

        public void DeleteChallenge(int id)
        {
            var challenge = data.Challenges
                .Include(c => c.Players)
                .FirstOrDefault(c => c.Id == id);

            challenge.Players.Clear();

            this.data.Remove(challenge);

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

        public void CreateChallenge(ChallengeFormModel challenge, int guestId, string hostUserId)
        {
            var challengeData = new Challenge
            {
                Name = challenge.Name,
                CourtType = challenge.CourtTypes,
                Sets = challenge.Sets,
                Games = challenge.Games,
                Rules = challenge.Rules,
                LastSets = challenge.LastSets,
                Description = challenge.Description,
                PlayerGuestId = guestId,
                PlayerHostUserId = hostUserId
            };

            this.data.Challenges.Add(challengeData);
            this.data.SaveChanges();


            var challengeId = challengeData.Id;

            AddPlayerToChallenge(hostUserId, guestId, challengeId);
        }
    }
}
