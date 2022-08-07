using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;
using TennisTournament.Services.Players;

namespace TennisTournament.Services.Tournaments
{
    public class TournamentQueryServiceModel
    {
        public int TotalTournaments { get; init; }

        public ICollection<TournamentServiceModel> Tournaments { get; init; }
    }
}
