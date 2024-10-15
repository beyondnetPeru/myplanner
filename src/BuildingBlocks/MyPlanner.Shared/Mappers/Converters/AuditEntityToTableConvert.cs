using AutoMapper;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Shared.Mappers.Converters
{
    public class AuditEntityToTableConvert : ITypeConverter<Audit, AuditTable>
    {
        public AuditTable Convert(Audit source, AuditTable destination, ResolutionContext context)
        {
            var dto = new AuditTable
            {
                CreatedBy = source.GetValue().CreatedBy,
                CreatedAt = source.GetValue().CreatedAt,
                UpdatedBy = source.GetValue().UpdatedBy!,
                UpdatedAt = source.GetValue().UpdatedAt,
                TimeSpan = source.GetValue().TimeSpan
            };

            return dto;
        }
    }
}
