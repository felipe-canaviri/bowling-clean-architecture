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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerModel>>> GetAll()
        {
            var players = await _playerService.FindAll();
            var models = _mapper.Map<IEnumerable<Player>, IEnumerable<PlayerModel>>(players);

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerModel>> Get(int id)
        {
            var player = await _playerService.FindById(id);
            var model = _mapper.Map<Player, PlayerModel>(player);

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<PlayerModel>> Create([FromBody] PlayerModel newPlayer)
        {
            var player = await _playerService.Save(_mapper.Map<PlayerModel, Player>(newPlayer));
            var model = _mapper.Map<Player, PlayerModel>(player);

            return CreatedAtAction(nameof(Get), new { id = player.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlayerModel>> Update(int id, [FromBody] PlayerModel updatedPlayer)
        {
            var player = await _playerService.Update(id, _mapper.Map<PlayerModel, Player>(updatedPlayer));

            return Ok(_mapper.Map<Player, PlayerModel>(player));
        }
    }
}
