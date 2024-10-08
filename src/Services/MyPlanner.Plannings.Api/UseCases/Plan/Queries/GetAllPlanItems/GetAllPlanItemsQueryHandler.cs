using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlanItems
{
    public class GetAllPlanItemsQueryHandler : AbstractQueryHandler<GetAllPlanItemsQuery, ResultSet>
    {
        private readonly IPlanQueryRepository planQueryRepository;
        private readonly IMapper mapper;

        public GetAllPlanItemsQueryHandler(IPlanQueryRepository planQueryRepository, IMapper mapper, ILogger<GetAllPlanItemsQueryHandler> logger):base(logger)
        {
            this.planQueryRepository = planQueryRepository ?? throw new ArgumentNullException(nameof(planQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleQuery(GetAllPlanItemsQuery request, CancellationToken cancellationToken)
        {
            var planItems = await planQueryRepository.GetPlanItems(request.PlanId);

            var dtos = mapper.Map<IEnumerable<PlanItemDto>>(planItems);

            return ResultSet.Success(dtos);
        }
    }
}
