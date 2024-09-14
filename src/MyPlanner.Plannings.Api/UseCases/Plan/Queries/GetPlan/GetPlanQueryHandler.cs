using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlan
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanDto>
    {
        private readonly IPlanRepository planRepository;
        private readonly IMapper mapper;

        public GetPlanQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            this.planRepository = planRepository;
            this.mapper = mapper;
        }

        public async Task<PlanDto> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            var dto = mapper.Map<PlanDto>(plan.GetPropsCopy());

            return dto;
        }
    }
}
