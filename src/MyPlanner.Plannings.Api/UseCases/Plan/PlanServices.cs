using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan
{
    public class PlanServices(
        IMediator mediator,
        IPlanRepository planRepository,
        IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public IPlanRepository PlanRepository { get; } = planRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
