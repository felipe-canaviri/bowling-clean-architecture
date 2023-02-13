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

        /// <summary>
        /// Retrieves the actual information for a specific turn given its identifier.
        /// </summary>
        /// <param name="id">The turn identifier.</param>
        /// <returns>An instance of TurnModel with the requested data.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TurnModel), 200)]
        public async Task<ActionResult> Get(int id)
        {
            var turn = await _turnService.FindById(id);
            var model = _mapper.Map<Turn, TurnModel>(turn);

            return Ok(model);
        }

        /// <summary>
        /// Creates a new turn for a player with the gaps to fill the different throwings. 
        /// </summary>
        /// <param name="model">Input with all the information of the Turn</param>
        /// <returns>An instance of TurnModel with the recently created data.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TurnModel), 200)]
        public async Task<ActionResult> Create([FromBody] TurnModel model)
        {
            var turn = await _turnService.Save(_mapper.Map<TurnModel, Turn>(model));
            var turnModel = _mapper.Map<Turn, TurnModel>(turn);

            return Ok(turnModel);
        }

        /// <summary>
        /// Allows to update an existing turn with information of new throwing's result.
        /// </summary>
        /// <param name="id">The Turn indentifier</param>
        /// <param name="model">The incoming new information for the Turn (new throwing's result).</param>
        /// <returns>An instance of TurnModel with the updated data.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TurnModel), 200)]
        public async Task<ActionResult> Update(int id, [FromBody] TurnModel model)
        {
            var game = await _turnService.Update(id, _mapper.Map<TurnModel, Turn>(model));

            return Ok(_mapper.Map<Turn, TurnModel>(game));
        }
    }
}
