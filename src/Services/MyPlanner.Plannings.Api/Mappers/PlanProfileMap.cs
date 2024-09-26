using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Infrastructure.Database.Tables;


namespace MyPlanner.Plannings.Api.Mappers
{
    public class PlanProfileMap : Profile
    {
        public PlanProfileMap()
        {
            CreateMap<int, PlanStatus>().ConvertUsing<EnumerationConverter<PlanStatus>>();
            CreateMap<int, PlanItemStatus>().ConvertUsing<EnumerationConverter<PlanItemStatus>>();


        }
    }
}
