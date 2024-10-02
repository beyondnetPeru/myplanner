using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Infrastructure.Database.Configurations
{
    public class SizeModelConfiguration : IEntityTypeConfiguration<SizeModelTable>
    {
        public void Configure(EntityTypeBuilder<SizeModelTable> builder)
        {
            builder.ToTable("sizemodels");
            builder.HasKey(p => p.Id);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.SizeModel)
                .HasForeignKey(x => x.SizeModelId);

            builder.OwnsOne(p => p.Audit).Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired();
            builder.OwnsOne(p => p.Audit).Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            builder.OwnsOne(p => p.Audit).Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false);
            builder.OwnsOne(p => p.Audit).Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false);
            builder.OwnsOne(p => p.Audit).Property(p => p.TimeSpan).HasColumnName("TimeSpan").IsRequired(false);
        }
    }
}
