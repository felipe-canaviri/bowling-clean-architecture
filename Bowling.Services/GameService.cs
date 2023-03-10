using Bowling.Core.Entities;
using Bowling.Core.Interfaces;
using Bowling.Core.Interfaces.Services;

namespace Bowling.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Game> Save(Game newGame)
        {
            await _unitOfWork.GameRepository.AddAsync(newGame);
            await _unitOfWork.CommitAsync();            

            return newGame;
        }

        public async Task Delete(int gameId)
        {
            Game game = await _unitOfWork.GameRepository.GetByIdAsync(gameId);
            _unitOfWork.GameRepository.Remove(game);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Game>> FindAll()
        {
            return await _unitOfWork.GameRepository.GetAllAsync();
        }

        public async Task<Game> FindById(int id)
        {
            return await _unitOfWork.GameRepository.GetByIdAsync(id);
        }

        public async Task<Game> Update(int GameToBeUpdatedId, Game newGameValues)
        {
            Game game = await _unitOfWork.GameRepository.GetByIdAsync(GameToBeUpdatedId);

            if (game == null)
                throw new ArgumentException("Invalid game ID while updating");

            game.Status = newGameValues.Status;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.GameRepository.GetByIdAsync(GameToBeUpdatedId);
        }
    }
}
