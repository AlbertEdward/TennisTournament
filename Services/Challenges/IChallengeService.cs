using TennisTournament.Models.Challenge;

namespace TennisTournament.Services.Challenges
{
    public interface IChallengeService
    {
        AllChallengesQueryModel Details(int id);

        void AddPlayerToChallenge(string userId, int challengeId);

        void RemovePlayerFromChallenge(string userId, int challengeId);

        void CreateChallenge(ChallengeFormModel challenge, int id, string hostId);
    }
}
