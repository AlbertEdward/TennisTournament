using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;
using TennisTournament.Services.Matches;

namespace TennisTournament.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        private readonly TennisDbContext data;

        public TournamentService(TennisDbContext data)
        {
            this.data = data;
        }
        public async Task<TournamentQueryServiceModel> AllAsync(
            string name,
            string searchTerm,
            CourtType courtType,
            GameType gameType)
        {
            var tournamentsQuery = this.data.Tournaments.AsQueryable();

            if (courtType != CourtType.Select)
            {
                tournamentsQuery = tournamentsQuery.Where(c => c.CourtType == courtType);
            }

            if (gameType != GameType.Select)
            {
                tournamentsQuery = tournamentsQuery.Where(g => g.GameType == gameType);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                tournamentsQuery = tournamentsQuery.Where(t =>
                    t.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    t.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalTournaments = await tournamentsQuery.CountAsync();

            var tournaments = tournamentsQuery
                .OrderByDescending(t => t.Id)
                .Select(t => new TournamentServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    MinRank = t.MinRank,
                    GameType = t.GameType,
                    CourtType = t.CourtType,
                    CoverPhoto = t.CoverPhoto,
                    StartDate = t.StartDate,
                    Description = t.Description,
                })
                .ToList();

            return new TournamentQueryServiceModel
            {
                Tournaments = tournaments,
                TotalTournaments = totalTournaments
            };
        }
        public async Task<TournamentServiceModel> DeleteAsync(int id)
        {
            var tournament = await this.data.Tournaments.FirstOrDefaultAsync(t => t.Id == id);

            data.Tournaments.Remove(tournament);

            data.SaveChangesAsync();

            return new TournamentServiceModel();
        }
        public async Task<bool> EditAsync(
            int id,
            string name,
            double? minRank,
            CourtType courtType,
            GameType gameType,
            Set set,
            Game game,
            Rule rule,
            LastSet lastSet,
            DateTime startDate,
            string description)
        {
            var tournamentData = await this.data.Tournaments.FindAsync(id);

            if (tournamentData == null)
            {
                return false;
            }

            //TODO make pictures editable
            tournamentData.Name = name;
            tournamentData.MinRank = minRank;
            tournamentData.CourtType = courtType;
            tournamentData.GameType = gameType;
            tournamentData.Set = set;
            tournamentData.Game = game;
            tournamentData.Rule = rule;
            tournamentData.LastSet = lastSet;
            tournamentData.StartDate = startDate;
            tournamentData.Description = description;

            this.data.SaveChangesAsync();

            return true;
        }
        public async Task<TournamentServiceModel> DetailsAsync(int id)
        => await this.data
            .Tournaments
            .Where(t => t.Id == id)
            .Select(tournament => new TournamentServiceModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                MinRank = tournament.MinRank,
                GameType = tournament.GameType,
                CourtType = tournament.CourtType,
                Set = tournament.Set,
                Game = tournament.Game,
                Rule = tournament.Rule,
                LastSet = tournament.LastSet,
                StartDate = tournament.StartDate,
                Description = tournament.Description,
                CoverPhoto = tournament.CoverPhoto,
                Players = tournament.Players,
                Matches = tournament.Match
            })
            .FirstOrDefaultAsync();

        public void AddPlayerToTournament(string userId, int tournamentId)
        {
            var player = data.Players.FirstOrDefault(p => p.UserId == userId);
            var tournament = data.Tournaments.Include(t => t.Players).FirstOrDefault(t => t.Id == tournamentId);

            if (player.Rank <= tournament.MinRank)
            {
                tournament.Players.Add(player);
            }
            else
            {
                throw new ArgumentOutOfRangeException(); // TODO Message -> "Your rank is lower for this tournament"
            }

            this.data.SaveChanges();
        }

        public void RemovePlayerFromTournament(string userId, int tournamentId)
        {
            var player = data.Players.Include(p => p.Tournaments).FirstOrDefault(p => p.UserId == userId);
            var tournament = data.Tournaments.FirstOrDefault(t => t.Id == tournamentId);

            player.Tournaments.Remove(tournament);
            this.data.SaveChanges();
        }
        public void CreateTournament(TournamentFormModel tournament, string coverPhoto)
        {
            var tournamentData = new Tournament
            {
                Name = tournament.Name,
                MinRank = tournament.MinRank,
                GameType = tournament.GameType,
                CourtType = tournament.CourtType,
                Set = tournament.Set,
                Game = tournament.Game,
                Rule = tournament.Rule,
                LastSet = tournament.LastSet,
                StartDate = tournament.StartDate,
                Description = tournament.Description,
                CoverPhoto = coverPhoto
            };

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();
        }
    }
}