using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Infrastructure.Database.Configurations
{
    public class PlanCategoryConfiguration : IEntityTypeConfiguration<PlanCategoryTable>
    {
        public void Configure(EntityTypeBuilder<PlanCategoryTable> builder)
        {
            builder.ToTable("plancategory");
            builder.HasKey(x => x.Id);
        }
    }
}
