using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Tournament;

namespace TennisTournament.Controllers.Api
{
    [ApiController]
    [Route("api/tournaments")]
    public class TournamentApiController : ControllerBase
    {
        private readonly TennisDbContext data;

        public TournamentApiController(TennisDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tournament>> GetTournaments()
        {
            return this.data.Tournaments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tournament> GetTournament(long id)
        {
            var tournament = this.data.Tournaments.Find(id);

            if (tournament == null) return NotFound();

            return tournament;
        }

        [HttpPost]
        public ActionResult<Tournament> PostTournament(TournamentListingViewModel tournamentModel)
        {
            this.data.Tournaments.Find(tournamentModel);

            return CreatedAtAction("GetTournament", new { id = tournamentModel.Id }, tournamentModel);
        }

        [HttpPut("{id}")]
        public IActionResult PutTournament(long id, TournamentListingViewModel tournamentModel)
        {
            if (id != tournamentModel.Id) return BadRequest();

            this.data.Tournaments.Find(id, tournamentModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Tournament> DeleteTournament(long id)
        {
            var tournament = this.data.Tournaments.Find(id);

            if (tournament == null) return NotFound();

            return tournament;
        }
    }
}
