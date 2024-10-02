using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Mappers
{
    public class PlanProfileMap : Profile
    {
        public PlanProfileMap()
        {
            CreateMap<int, PlanStatus>().ConvertUsing<EnumerationConverter<PlanStatus>>();
            CreateMap<int, PlanItemStatus>().ConvertUsing<EnumerationConverter<PlanItemStatus>>();
            CreateMap<Audit, AuditTable>().ConvertUsing(new AuditToAuditTableConvert());
            CreateMap<CreatePlanDto, AddPlanItemRequest>();
        }
    }
}
