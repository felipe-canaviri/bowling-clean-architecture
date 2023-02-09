using AutoMapper;
using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Services;
using Bowling.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all existing games in the app.
        /// </summary>
        /// <returns>A collection of GameModel.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GameModel>), 200)]
        public async Task<ActionResult> GetAll()
        {
            var games = await _gameService.FindAll();
            var models = _mapper.Map<IEnumerable<Game>, IEnumerable<GameModel>>(games);

            return Ok(models);
        }

        /// <summary>
        /// Retrieves the data of a single game given its identifier.
        /// </summary>
        /// <param name="id">the game identifier to be recovered.</param>
        /// <returns>An instance of GameModel</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GameModel), 200)]
        public async Task<ActionResult> Get(int id)
        {
            var game = await _gameService.FindById(id);
            var model = _mapper.Map<Game, GameModel>(game);

            return Ok(model);
        }

        /// <summary>
        /// Creates a new game in the app. It's not required any input for this action.
        /// </summary>
        /// <returns>An instance of GameModel.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GameModel), 200)]
        public async Task<ActionResult> Create()
        {
            var game = new Game() { 
                CreatedAt = DateTime.UtcNow,
                Status = (int)GameStatus.New
            };
            game = await _gameService.Save(game);
            var model = _mapper.Map<Game, GameModel>(game);

            return Ok(model);
        }

        /// <summary>
        /// Updates the data for a given game.
        /// </summary>
        /// <param name="id">The game identifier to be updated.</param>
        /// <param name="gameModel">The new data for the existing game.</param>
        /// <returns>An instance of GameModel with the new updated data.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GameModel), 200)]
        public async Task<ActionResult> Update(int id, [FromBody] GameModel gameModel)
        {
            var game = await _gameService.Update(id, _mapper.Map<GameModel, Game>(gameModel));

            return Ok(_mapper.Map<Game, GameModel>(game));
        }
    }
}
