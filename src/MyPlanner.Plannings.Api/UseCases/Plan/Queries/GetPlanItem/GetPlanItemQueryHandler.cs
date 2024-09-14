using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlanItem
{
    public class GetPlanItemQueryHandler : IRequestHandler<GetPlanItemQuery, PlanDto>
    {
        private readonly IPlanRepository planRepository;
        private readonly IMapper mapper;

        public GetPlanItemQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PlanDto> Handle(GetPlanItemQuery request, CancellationToken cancellationToken)
        {
            var query = await planRepository.GetItemById(request.PlanId, request.PlanItemId);

            var dto = mapper.Map<PlanDto>(query);

            return dto;
        }
    }
}
