using AutoMapper;
using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Services;
using Bowling.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlayerService _playerService;

        public PlayerController(IMapper mapper, IPlayerService playerService)
        {
            _mapper = mapper;
            _playerService = playerService;
        }

        /// <summary>
        /// Retrieves all players existing in the app.
        /// </summary>
        /// <returns>A collection of PlayerModel</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerModel>>> GetAll()
        {
            var players = await _playerService.FindAll();
            var models = _mapper.Map<IEnumerable<Player>, IEnumerable<PlayerModel>>(players);

            return Ok(models);
        }

        /// <summary>
        /// Retrieves the actual information for a specific player given its identifier.
        /// </summary>
        /// <param name="id">The player identifier.</param>
        /// <returns>An instance of PlayerModel with the requested data.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerModel>> Get(int id)
        {
            var player = await _playerService.FindById(id);
            var model = _mapper.Map<Player, PlayerModel>(player);

            return Ok(model);
        }

        /// <summary>
        /// Creates a new player in the app. This player will belong to the specified game.
        /// </summary>
        /// <param name="newPlayer">Input with all the information of the player</param>
        /// <returns>An instance of PlayerModel with the recently created data.</returns>
        [HttpPost]
        public async Task<ActionResult<PlayerModel>> Create([FromBody] PlayerModel newPlayer)
        {
            var player = await _playerService.Save(_mapper.Map<PlayerModel, Player>(newPlayer));
            var model = _mapper.Map<Player, PlayerModel>(player);

            return CreatedAtAction(nameof(Get), new { id = player.Id }, model);
        }

        /// <summary>
        /// Updates the information from a given player.
        /// </summary>
        /// <param name="id">The player indentifier</param>
        /// <param name="updatedPlayer">The incoming new information for the player.</param>
        /// <returns>An instance of PlayerModel with the updated data.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PlayerModel>> Update(int id, [FromBody] PlayerModel updatedPlayer)
        {
            var player = await _playerService.Update(id, _mapper.Map<PlayerModel, Player>(updatedPlayer));

            return Ok(_mapper.Map<Player, PlayerModel>(player));
        }
    }
}
