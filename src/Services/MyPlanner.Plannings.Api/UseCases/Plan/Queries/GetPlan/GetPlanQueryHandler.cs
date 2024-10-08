using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlan
{
    public class GetPlanQueryHandler : AbstractQueryHandler<GetPlanQuery, ResultSet>
    {
        private readonly IPlanRepository planRepository;
        private readonly IMapper mapper;

        public GetPlanQueryHandler(IPlanRepository planRepository, IMapper mapper, ILogger<GetPlanQueryHandler> logger) : base(logger)
        {
            this.planRepository = planRepository;
            this.mapper = mapper;
        }

        public override async Task<ResultSet> HandleQuery(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            var dto = mapper.Map<PlanDto>(plan.GetPropsCopy());

            return ResultSet.Success(dto);
        }
    }
}
