using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Repositories;
using Bowling.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bowling.Infrastructure.Repositories
{
    public class ScoresRepository : IScoresRepository
    {
        internal AppDbContext Context;
        internal DbSet<Scores> dbSet;

        public ScoresRepository(AppDbContext context)
        {
            Context = context;
            dbSet = context.Set<Scores>();
        }

        public virtual async Task<IEnumerable<Scores>> GetScoresByGameAndPlayerAsync(int gameId, int playerId)
        {
            IQueryable<Scores> query = dbSet;
            query = query.Where(x => x.playerid == playerId && x.GameId == gameId);

            return await query.ToListAsync();
        }
    }
}
