using AutoMapper;
using MyPlanner.Shared.Application.Dtos;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Shared.Mappers.Converters
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
