using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPlanner.Plannings.Infrastructure.Database.Tables;


namespace MyPlanner.Products.Infrastructure.Database.Configurations
{
    public class SizeModelTypeConfiguration : IEntityTypeConfiguration<SizeModelTypeTable>
    {
        public void Configure(EntityTypeBuilder<SizeModelTypeTable> builder)
        {
            builder.ToTable("sizemodeltypes");
            builder.HasKey(x => x.Id);
        }
    }
}
