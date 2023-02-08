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

        // GET: GameController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameModel>>> GetAll()
        {
            var games = await _gameService.FindAll();
            var models = _mapper.Map<IEnumerable<Game>, IEnumerable<GameModel>>(games);

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameModel>> Get(int id)
        {
            var game = await _gameService.FindById(id);
            var model = _mapper.Map<Game, GameModel>(game);

            return Ok(model);
        }

        [HttpGet("{id}/player/{playerId}")]
        public async Task<ActionResult<IEnumerable<Scores>>> GetScores(int id, int playerId)
        {
            var scores = await _gameService.GetScore(id, playerId);            

            return Ok(scores);
        }

        // POST: GameController/Create
        [HttpPost]        
        public async Task<ActionResult<GameModel>> Create()
        {
            var game = new Game() { 
                CreatedAt = DateTime.UtcNow,
                Status = (int)GameStatus.New
            };
            game = await _gameService.Save(game);
            var model = _mapper.Map<Game, GameModel>(game);

            return CreatedAtAction(nameof(Get), new { id = game.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GameModel>> Update(int id, [FromBody] GameModel gameModel)
        {
            var game = await _gameService.Update(id, _mapper.Map<GameModel, Game>(gameModel));

            return Ok(_mapper.Map<Game, GameModel>(game));
        }
    }
}
