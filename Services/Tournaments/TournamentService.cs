using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;

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
            string description,
            string coverPhoto)
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
            tournamentData.Sets = set;
            tournamentData.Games = game;
            tournamentData.Rules = rule;
            tournamentData.LastSets = lastSet;
            tournamentData.Description = description;
            tournamentData.CoverPhoto = coverPhoto;

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
                Set = tournament.Sets,
                Game = tournament.Games,
                Rule = tournament.Rules,
                LastSet = tournament.LastSets,
                Description = tournament.Description,
                CoverPhoto = tournament.CoverPhoto

            })
            .FirstOrDefaultAsync();

        public void AddPlayerToTournament(string userId, int tournamentId)
        {
            var player = data.Players.FirstOrDefault(p => p.UserId == userId);
            var tournament = data.Tournaments.FirstOrDefault(t => t.Id == tournamentId);

            if (!tournament.Player.Any())
            {
                tournament.Player.Add(player);
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
                GameType = tournament.GameTypes,
                CourtType = tournament.CourtTypes,
                Sets = tournament.Sets,
                Games = tournament.Games,
                Rules = tournament.Rules,
                LastSets = tournament.LastSets,
                Description = tournament.Description,
                CoverPhoto = coverPhoto
            };

            this.data.Tournaments.Add(tournamentData);
            this.data.SaveChanges();
        }
    }
}