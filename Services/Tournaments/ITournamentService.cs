using TennisTournament.Data.Models;

namespace TennisTournament.Services.Tournaments
{
    public interface ITournamentService
    {
        TournamentQueryServiceModel All(
            string Name,
            string SearchTerm,
            CourtType courtType,
            GameType gameType);
    }
}
