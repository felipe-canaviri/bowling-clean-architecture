using Bowling.Core.Interfaces;
using Bowling.Core.Interfaces.Repositories;
using Bowling.Infrastructure.Repositories;

namespace Bowling.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private GameRepository _gameRepository;
        private PlayerRepository _playerRepository;
        private TurnRepository _turnRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository => _gameRepository ??= new GameRepository(_context);

        public IPlayerRepository PlayerRepository => _playerRepository??= new PlayerRepository(_context);

        public ITurnRepository TurnRepository => _turnRepository ??= new TurnRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
