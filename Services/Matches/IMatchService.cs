using TennisTournament.Services.Matches.Models;

namespace TennisTournament.Services.Matches
{
    public interface IMatchService
    {
        void CreateMatch(MatchServiceModel match, int id);
    }
}
