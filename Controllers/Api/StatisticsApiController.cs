using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Models.Api.Statistics;

namespace TennisTournament.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly TennisDbContext data;

        public StatisticsApiController(TennisDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalTournaments = this.data.Tournaments.Count();
            var totalPlayers = this.data.Players.Count();

            var statistics = new StatisticsResponseModel
            {
                TotalTournaments = totalTournaments,
                TotalPlayers = totalPlayers
            };

            return statistics;
        }
    }
}
