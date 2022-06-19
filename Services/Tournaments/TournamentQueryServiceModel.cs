using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;

namespace TennisTournament.Services.Tournaments
{
    public class TournamentQueryServiceModel
    {
        public int TotalTournaments { get; init; }
        public IEnumerable<TournamentServiceModel> Tournaments { get; init; }
    }
}
