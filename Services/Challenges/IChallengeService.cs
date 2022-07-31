namespace TennisTournament.Services.Challenges
{
    public interface IChallengeService
    {
        void AddPlayerToChallenge(string userId, int challengeId);
        void RemovePlayerFromChallenge(string userId, int challengeId);
    }
}
