using TennisTournament.Models.Challenge;
using TennisTournament.Services.Challenges.Models;

namespace TennisTournament.Services.Challenges
{
    public interface IChallengeService
    {
        ChallengeServiceModel Details(int id);

        void AddPlayerToChallenge(string playerHostUserId, int id, int challengeId);

        void DeleteChallenge(int id);

        void CreateChallenge(ChallengeFormModel challenge, int id, string hostId);

        void ChallengeResult(int winnerId, int loserId);
    }
}
