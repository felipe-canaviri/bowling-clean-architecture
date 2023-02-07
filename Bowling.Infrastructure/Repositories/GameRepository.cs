using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Repositories;
using Bowling.Infrastructure.Data;

namespace Bowling.Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext context) : base(context)
        {
        }
    }
}
