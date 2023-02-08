using Bowling.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bowling.Infrastructure.Data.Configurations
{
    public class TurnConfiguration : IEntityTypeConfiguration<Turn>
    {
        public void Configure(EntityTypeBuilder<Turn> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FirstThrowing);
            builder.Property(x => x.SecondThrowing);
            builder.Property(x => x.ThirdThrowing);
            builder.Property(x => x.TurnNumber);

            builder.ToTable("Turns");
        }
    }
}
