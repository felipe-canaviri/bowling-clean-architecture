using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Repositories;
using Bowling.Infrastructure.Data;

namespace Bowling.Infrastructure.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
