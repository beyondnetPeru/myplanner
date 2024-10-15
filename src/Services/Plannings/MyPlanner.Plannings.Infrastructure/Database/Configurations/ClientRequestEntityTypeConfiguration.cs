using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Infrastructure.Database.Configurations
{
    public class ClientRequestEntityTypeConfiguration
        : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
        {
            requestConfiguration.ToTable("requests");
        }
    }
}
