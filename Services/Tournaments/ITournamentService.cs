using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;

namespace TennisTournament.Services.Tournaments
{
    public interface ITournamentService
    {
        Task<TournamentQueryServiceModel> AllAsync(
            string Name,
            string SearchTerm,
            CourtType courtType,
            GameType gameType);

        Task<bool> EditAsync(
            int id,
            string name,
            double? minRank,
            CourtType courtType,
            GameType gameType,
            Set set,
            Game game,
            Rule rule,
            LastSet lastSet,
            string description);

        Task<TournamentServiceModel> DeleteAsync(int id);

        Task<TournamentServiceModel> DetailsAsync(int id);

        void AddPlayerToTournament(string userId, int tournamentId);

        void RemovePlayerFromTournament(string userId, int tournamentId);

        void CreateTournament(TournamentFormModel tournament, string coverPhoto);
    }
}
