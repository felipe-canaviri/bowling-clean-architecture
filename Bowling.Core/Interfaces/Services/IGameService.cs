using Bowling.Core.Entities;

namespace Bowling.Core.Interfaces.Services
{
    public interface IGameService
    {
        Task<Game> GetGameById(int id);
        Task<IEnumerable<Game>> GetAll();
        Task<Game> CreateGame(Game newGame);
        Task<Game> UpdateGame(int GameToBeUpdatedId, Game newGameValues);
        Task DeleteGame(int GameId);
    }
}
