using Bowling.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bowling.Infrastructure.Data.Configurations
{
    public class ScoresConfiguration : IEntityTypeConfiguration<Scores>
    {
        public void Configure(EntityTypeBuilder<Scores> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.GameId);
            builder.Property(x => x.playerid);
            builder.Property(x => x.Name);
            builder.Property(x => x.score);            

            builder.ToTable("scores");
        }
    }
}
