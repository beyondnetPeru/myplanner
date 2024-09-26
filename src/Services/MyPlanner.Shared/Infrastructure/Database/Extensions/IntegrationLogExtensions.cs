using Microsoft.EntityFrameworkCore;
using MyPlanner.IntegrationEventLogEF;

namespace MyPlanner.Shared.Infrastructure.Database.Extensions
{
    public static class IntegrationLogExtensions
    {
        public static void UseIntegrationEventLogs(this ModelBuilder builder)
        {
            builder.Entity<IntegrationEventLogEntry>(builder =>
            {
                builder.ToTable("IntegrationEventLog");

                builder.HasKey(e => e.EventId);
            });
        }
    }
}
