using AutoMapper;
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
        /// </summary>
        /// <param name="id">The game identifier.</param>
        /// <param name="playerId">The player id </param>
        /// <returns>A collection of scores for a given player</returns>
        [HttpGet("{id}/player/{playerId}")]
        public async Task<ActionResult<IEnumerable<Scores>>> GetScores(int id, int playerId)
        {
            var scores = await _scoreService.GetScore(id, playerId);

            return Ok(scores);
        }
    }
}
