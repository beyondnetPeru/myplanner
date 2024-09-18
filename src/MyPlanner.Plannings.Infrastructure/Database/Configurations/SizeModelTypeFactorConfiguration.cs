using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPlanner.Plannings.Infrastructure.Database.Tables;


namespace MyPlanner.Products.Infrastructure.Database.Configurations
{
    public class SizeModelTypeFactorConfiguration : IEntityTypeConfiguration<SizeModelTypeFactorTable>
    {
        public void Configure(EntityTypeBuilder<SizeModelTypeFactorTable> builder)
        {
            builder.ToTable("sizemodeltypefactors");
            builder.HasKey(x => x.Id);
        }
    }
}
