using Bowling.Core.Entities;

namespace Bowling.Core.Interfaces.Services
{
    public interface IGameService
    {
        Task<Game> FindById(int id);
        Task<IEnumerable<Game>> FindAll();
        Task<Game> Save(Game newGame);
        Task<Game> Update(int gameToBeUpdatedId, Game newGameValues);
        Task Delete(int gameId);

        Task<IEnumerable<Scores>> GetScore(int gameId, int playerId);
    }
}
