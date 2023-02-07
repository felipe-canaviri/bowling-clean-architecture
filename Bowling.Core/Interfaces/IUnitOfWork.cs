using Bowling.Core.Interfaces.Repositories;

namespace Bowling.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository GameRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        ITurnRepository TurnRepository { get; }

        Task<int> CommitAsync();
    }
}
