using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan
{
    public class PlanItemServices(
        IMediator mediator,
        IPlanItemRepository planItemRepository,
        IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public IPlanItemRepository PlanItemRepository { get; } = planItemRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
