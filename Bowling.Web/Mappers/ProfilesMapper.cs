using AutoMapper;
using Bowling.Core.Entities;
using Bowling.Web.Models;

namespace Bowling.Web.Mappers
{
    public class ProfilesMapper : Profile
    {
        public ProfilesMapper()
        {
            CreateMap<Game, GameModel>();
            CreateMap<Player, PlayerModel>();
            CreateMap<Turn, TurnModel>();
            
            CreateMap<GameModel, Game>();
            CreateMap<PlayerModel, Player>();
            CreateMap<TurnModel, Turn>();
        }
    }
}
