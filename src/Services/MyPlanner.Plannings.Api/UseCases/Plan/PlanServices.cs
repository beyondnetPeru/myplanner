using MyPlanner.Plannings.Api.UseCases.Plan.Queries;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan
{
    public class PlanServices(
        IMediator mediator,
        IPlanRepository planRepository,
        IPlanQueryRepository planQueryRepository,
        IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public IPlanRepository PlanRepository { get; } = planRepository;
        public IPlanQueryRepository PlanQueryRepository { get; } = planQueryRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
