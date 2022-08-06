using TennisTournament.Models.Challenge;

namespace TennisTournament.Services.Challenges
{
    public interface IChallengeService
    {
        AllChallengesQueryModel Details(int id);

        void AddPlayerToChallenge(string playerHostUserId, int id, int challengeId);

        void RemovePlayerFromChallenge(int challengeId);

        void CreateChallenge(ChallengeFormModel challenge, int id, string hostId);
    }
}
