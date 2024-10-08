
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlanItem
{
    public class GetPlanItemQueryHandler : AbstractQueryHandler<GetPlanItemQuery, ResultSet>
    {
        private readonly IPlanRepository planRepository;
        private readonly IMapper mapper;

        public GetPlanItemQueryHandler(IPlanRepository planRepository, IMapper mapper, ILogger<GetPlanItemQueryHandler> logger) : base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleQuery(GetPlanItemQuery request, CancellationToken cancellationToken)
        {
            var query = await planRepository.GetItemById(request.PlanItemId);

            var dto = mapper.Map<PlanDto>(query);

            return ResultSet.Success(dto);
        }
    }
}
