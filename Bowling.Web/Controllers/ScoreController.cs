using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;        

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        /// <summary>
        /// Recovers the current score for a given player in the current game.
        /// This endpoint is being cached in case there are very frequent request in a short period of time.
        /// </summary>
        /// <param name="id">The game identifier.</param>
        /// <param name="playerId">The player id </param>
        /// <returns>A collection of scores for a given player</returns>
        [HttpGet("{id}/player/{playerId}")]
        [ProducesResponseType(typeof(IEnumerable<Scores>), 200)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "id", "playerId" })]
        public async Task<ActionResult> GetScores(int id, int playerId)
        {
            var scores = await _scoreService.GetScore(id, playerId);

            return Ok(scores);
        }
    }
}
