﻿using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;

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

        TournamentServiceModel Details(int id);

        void AddPlayerToTournament(string playerId, int tournamentId);

        void RemovePlayerFromTournament(string playerId, int tournamentId);

        void AddTournament(TournamentFormModel tournament, string coverPhoto);
    }
}
