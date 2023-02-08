using Bowling.Core.Entities;

namespace Bowling.Core.Interfaces.Services
{
    
    public interface IPlayerService
    {
        Task<Player> GetPlayerById(int id);
        Task<IEnumerable<Player>> GetAll();
        Task<IEnumerable<Player>> GetAllByGameId(int gameId);
        Task<Player> CreatePlayer(Player player);
        Task<Player> UpdatePlayer(int playerToBeUpdatedId, Player newPlayerValues);
        Task DeleteGame(int playerId);
    }
}
