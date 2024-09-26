using AutoMapper;
using MyPlanner.Plannings.Shared.Application.Dtos;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Shared.Mappers.Converters
{
    public class AuditToAuditDtoConvert : ITypeConverter<Audit, AuditDto>
    {
        public AuditDto Convert(Audit source, AuditDto destination, ResolutionContext context)
        {
            var dto = new AuditDto
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
