using TennisTournament.Services.Rounds.Models;

namespace TennisTournament.Services.Rounds
{
    public interface IRoundService
    {
        void CreateRound(RoundServiceModel round, int tournamentId);
    }
}
