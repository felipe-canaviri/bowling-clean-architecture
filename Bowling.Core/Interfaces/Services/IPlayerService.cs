using Bowling.Core.Entities;

namespace Bowling.Core.Interfaces.Services
{    
    public interface IPlayerService
    {
        Task<Player> FindById(int id);
        Task<IEnumerable<Player>> FindAll();
        Task<Player> Save(Player player);
        Task<Player> Update(int playerToBeUpdatedId, Player newPlayerValues);
        Task Delete(int playerId);

        Task<IEnumerable<Player>> FindAllByGameId(int gameId);
    }
}
