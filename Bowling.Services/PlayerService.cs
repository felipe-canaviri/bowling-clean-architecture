using Bowling.Core.Entities;
using Bowling.Core.Interfaces;
using Bowling.Core.Interfaces.Services;

namespace Bowling.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Save(Player player)
        {
            await _unitOfWork.PlayerRepository.AddAsync(player);
            await _unitOfWork.CommitAsync();

            return player;
        }

        public async Task Delete(int playerId)
        {
            var player = await _unitOfWork.PlayerRepository.GetByIdAsync(playerId);
            _unitOfWork.PlayerRepository.Remove(player);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Player>> FindAll()
        {
            return await _unitOfWork.PlayerRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Player>> FindAllByGameId(int gameId)
        {
            var players = await _unitOfWork.PlayerRepository.GetAsync(filter: p => p.GameId == gameId);
            return players;
        }

        public async Task<Player> FindById(int id)
        {
            return await _unitOfWork.PlayerRepository.GetByIdAsync(id);
        }

        public async Task<Player> Update(int playerToBeUpdatedId, Player newPlayerValues)
        {
            Player player = await _unitOfWork.PlayerRepository.GetByIdAsync(playerToBeUpdatedId);

            if (player == null)
                throw new ArgumentException("Invalid player ID while updating");

            player.Name = newPlayerValues.Name;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.PlayerRepository.GetByIdAsync(playerToBeUpdatedId);
        }
    }
}
