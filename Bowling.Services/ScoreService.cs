using Bowling.Core.Entities;
using Bowling.Core.Interfaces;
using Bowling.Core.Interfaces.Services;

namespace Bowling.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Scores>> GetScore(int gameId, int playerId)
        {
            return await _unitOfWork.ScoresRepository.GetScoresByGameAndPlayerAsync(gameId, playerId);
        }
    }
}
