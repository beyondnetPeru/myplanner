using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPlanner.Plannings.Infrastructure.Database.Tables;


namespace MyPlanner.Plannings.Infrastructure.Database.Configurations
{
    public class PlanItemConfiguration : IEntityTypeConfiguration<PlanItemTable>
    {
        public void Configure(EntityTypeBuilder<PlanItemTable> builder)
        {
            builder.ToTable("planitems");
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Plan)
                   .WithMany(x => x.Items);

            builder.OwnsOne(p => p.Audit).Property(p => p.CreatedBy).HasColumnName("CreatedBy").IsRequired();
            builder.OwnsOne(p => p.Audit).Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            builder.OwnsOne(p => p.Audit).Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").IsRequired(false);
            builder.OwnsOne(p => p.Audit).Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false);
            builder.OwnsOne(p => p.Audit).Property(p => p.TimeSpan).HasColumnName("TimeSpan").IsRequired(false);
        }
    }
}
