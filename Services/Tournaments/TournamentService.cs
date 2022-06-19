using TennisTournament.Data;
using TennisTournament.Data.Models;

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
    }
}
