using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Infrastructure.Database.Configurations
{
    public class ErrorEntityTypeConfiguration : IEntityTypeConfiguration<ErrorTable>
    {
        public void Configure(EntityTypeBuilder<ErrorTable> builder)
        {
            builder.ToTable("errors");
            builder.HasKey(x => x.Id);
        }
    }
}
