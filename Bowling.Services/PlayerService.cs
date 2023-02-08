using Bowling.Core.Entities;
using Bowling.Core.Interfaces;
using Bowling.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _unitOfWork.PlayerRepository.AddAsync(player);
            await _unitOfWork.CommitAsync();

            return player;
        }

        public async Task DeleteGame(int playerId)
        {
            var player = await _unitOfWork.PlayerRepository.GetByIdAsync(playerId);
            _unitOfWork.PlayerRepository.Remove(player);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await _unitOfWork.PlayerRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Player>> GetAllByGameId(int gameId)
        {
            var players = await _unitOfWork.PlayerRepository.GetAsync(filter: p => p.GameId == gameId);
            return players;
        }

        public async Task<Player> GetPlayerById(int id)
        {
            return await _unitOfWork.PlayerRepository.GetByIdAsync(id);
        }

        public Task<Player> UpdateGame(int playerToBeUpdatedId, Player newPlayerValues)
        {
            throw new NotImplementedException();
        }
    }
}
