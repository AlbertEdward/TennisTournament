using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Services.Players;

namespace TennisTournament.Controllers.Api
{
    [ApiController]
    [Route("api/players")]
    public class PlayersApiController : ControllerBase
    {
        private readonly TennisDbContext data;
        private readonly IPlayerService playerService;

        public PlayersApiController(TennisDbContext data)
        {
            this.data=data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return this.data.Players.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlayerAsync(int id)
        {
            var player = await this.data.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdatePlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            this.data.Players.Add(player);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Player> DeletePlayer(int id)
        {
            var player = this.playerService.Delete(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }
    }
}