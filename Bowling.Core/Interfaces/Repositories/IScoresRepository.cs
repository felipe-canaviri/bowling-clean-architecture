using Bowling.Core.Entities;

namespace Bowling.Core.Interfaces.Repositories
{
    public interface IScoresRepository
    {
        Task<IEnumerable<Scores>> GetScoresByGameAndPlayerAsync(int gameId, int playerId);
    }
}
