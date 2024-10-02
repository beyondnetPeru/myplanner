using MyPlanner.Plannings.Infrastructure.Database.Tables;


namespace MyPlanner.Products.Infrastructure.Database.Configurations
{
    public class SizeModelTypeConfiguration : IEntityTypeConfiguration<SizeModelTypeTable>
    {
        public void Configure(EntityTypeBuilder<SizeModelTypeTable> builder)
        {
            builder.ToTable("sizemodeltypes");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.SizeModelType)
                .HasForeignKey(x => x.SizeModelTypeId);
        }
    }
}
