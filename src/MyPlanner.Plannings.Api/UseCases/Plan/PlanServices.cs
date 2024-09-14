using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan
{
    public class PlanServices(
        IMediator mediator,
        IPlanItemRepository planItemRepository,
        IPlanRepository planRepository,
        IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public IPlanRepository PlanRepository { get; } = planRepository;

        public IPlanItemRepository PlanItemRepository { get; } = planItemRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
