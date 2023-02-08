using AutoMapper;
using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Services;
using Bowling.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnController : ControllerBase
    {
        private readonly ITurnService _turnService;
        private readonly IMapper _mapper;

        public TurnController(ITurnService turnService, IMapper mapper)
        {
            _turnService = turnService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurnModel>> Get(int id)
        {
            var turn = await _turnService.FindById(id);
            var model = _mapper.Map<Turn, TurnModel>(turn);

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<TurnModel>> Create([FromBody] TurnModel model)
        {
            var turn = await _turnService.Save(_mapper.Map<TurnModel, Turn>(model));
            var turnModel = _mapper.Map<Turn, TurnModel>(turn);

            return CreatedAtAction(nameof(Get), new { id = turn.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TurnModel>> Update(int id, [FromBody] TurnModel model)
        {
            var game = await _turnService.Update(id, _mapper.Map<TurnModel, Turn>(model));

            return Ok(_mapper.Map<Turn, TurnModel>(game));
        }
    }
}
