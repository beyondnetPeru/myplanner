using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPlanner.Plannings.Infrastructure.Database.Tables;


namespace MyPlanner.Products.Infrastructure.Database.Configurations
{
    public class SizeModelTypeItemsConfiguration : IEntityTypeConfiguration<SizeModelTypeItemTable>
    {
        public void Configure(EntityTypeBuilder<SizeModelTypeItemTable> builder)
        {
            builder.ToTable("sizemodeltypeitems");
            builder.HasKey(x => x.Id);


            builder.HasOne(x => x.SizeModelType)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.SizeModelTypeId);
        }
    }
}
