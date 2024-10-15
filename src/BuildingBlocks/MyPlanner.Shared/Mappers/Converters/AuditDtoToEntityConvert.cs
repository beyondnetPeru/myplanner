using AutoMapper;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Shared.Mappers.Converters
{
    public class AuditDtoToEntityConvert : ITypeConverter<AuditDto, Audit>
    {
        public Audit Convert(AuditDto source, Audit destination, ResolutionContext context)
        {
            var props = new AuditProps() {
                CreatedBy = source.CreatedBy,
                CreatedAt = source.CreatedAt,
                UpdatedBy = source.UpdatedBy,
                UpdatedAt = source.UpdatedAt,
                TimeSpan = source.TimeSpan.ToString()
            };

            var audit = Audit.Load(props);

            return audit;
        }
    }
}
