using Bowling.Core.Entities;

namespace Bowling.Core.Interfaces.Services
{
    public interface IScoreService
    {
        Task<IEnumerable<Scores>> GetScore(int gameId, int playerId);
    }
}
