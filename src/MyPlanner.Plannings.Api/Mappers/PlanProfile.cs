using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Infrastructure.Database.Tables;


namespace MyPlanner.Plannings.Api.Mappers
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<int, PlanStatus>().ConvertUsing<EnumerationConverter<PlanStatus>>();
            CreateMap<int, PlanItemStatus>().ConvertUsing<EnumerationConverter<PlanItemStatus>>();


        }
    }
}
