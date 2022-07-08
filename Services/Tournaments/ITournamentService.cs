using TennisTournament.Data.Models;
using TennisTournament.Services.Tournaments.Models;

namespace TennisTournament.Services.Tournaments
{
    public interface ITournamentService
    {
        TournamentQueryServiceModel All(
            string Name,
            string SearchTerm,
            CourtType courtType,
            GameType gameType);

        bool Edit(
            int id,
            string name,
            CourtType courtType,
            GameType gameType,
            Set set,
            Game game,
            Rule rule,
            LastSet lastSet,
            string description);

        Task<TournamentServiceModel> Delete(int id);

        TournamentDetailsServiceModel Details(int id);
    }
}
