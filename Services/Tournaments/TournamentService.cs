using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Services.Tournaments.Models;

namespace TennisTournament.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        private readonly TennisDbContext data;

        public TournamentService(TennisDbContext data)
        {
            this.data = data;
        }

        public TournamentQueryServiceModel All(
            string name,
            string searchTerm,
            CourtType courtType,
            GameType gameType
            )
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

            var totalTournaments = tournamentsQuery.Count();

            var tournaments = tournamentsQuery
                .OrderByDescending(t => t.Id)
                .Select(t => new TournamentServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    GameType = t.GameType,
                    CourtType = t.CourtType,
                    CoverPhoto = t.CoverPhoto
                })
                .ToList();

            return new TournamentQueryServiceModel
            {
                Tournaments = tournaments,
                TotalTournaments = totalTournaments
            };
        }

        public TournamentServiceModel Delete(int id)
        {
            var tournament = this.data.Tournaments.FirstOrDefault(t => t.Id == id);

            data.Tournaments.Remove(tournament);

            data.SaveChanges();

            return new TournamentServiceModel();
        }

        public bool Edit(
            int id,
            string name,
            CourtType courtType,
            GameType gameType,
            Set set,
            Game game,
            Rule rule,
            LastSet lastSet,
            string description
            )
        {
            var tournamentData = this.data.Tournaments.Find(id);

            if (tournamentData == null)
            {
                return false;
            }

            //TODO make pictures editable
            tournamentData.Name = name;
            tournamentData.CourtType = courtType;
            tournamentData.GameType = gameType;
            tournamentData.Sets = set;
            tournamentData.Games = game;
            tournamentData.Rules = rule;
            tournamentData.LastSets = lastSet;
            tournamentData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public TournamentDetailsServiceModel Details(int id)
        => this.data
            .Tournaments
            .Where(t => t.Id == id)
            .Select(tournament => new TournamentDetailsServiceModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                GameType = tournament.GameType,
                CourtType = tournament.CourtType,
                Set = tournament.Sets,
                Game = tournament.Games,
                Rule = tournament.Rules,
                LastSet = tournament.LastSets,
                Description = tournament.Description,
                CoverPhoto = tournament.CoverPhoto

            })
            .FirstOrDefault();
    }
}
