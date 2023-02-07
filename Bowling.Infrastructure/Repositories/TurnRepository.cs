using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Repositories;
using Bowling.Infrastructure.Data;

namespace Bowling.Infrastructure.Repositories
{
    public class TurnRepository : BaseRepository<Turn>, ITurnRepository
    {
        public TurnRepository(AppDbContext context) : base(context)
        {
        }
    }
}
