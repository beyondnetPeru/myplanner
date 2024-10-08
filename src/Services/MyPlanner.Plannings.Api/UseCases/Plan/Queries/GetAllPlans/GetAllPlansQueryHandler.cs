using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansQueryHandler : AbstractQueryHandler<GetAllPlansQuery, ResultSet>
    {
        private readonly IPlanQueryRepository planQueryRepository;
        private readonly IMapper mapper;

        public GetAllPlansQueryHandler(IPlanQueryRepository planQueryRepository, IMapper mapper, ILogger<GetAllPlansQueryHandler> logger):base(logger)
        {
            this.planQueryRepository = planQueryRepository ?? throw new ArgumentNullException(nameof(planQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleQuery(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var entities = await planQueryRepository.GetPlans(request.Pagination);

            var dtos = mapper.Map<IEnumerable<PlanDto>>(entities);

            return ResultSet.Success(dtos);
        }
    }
}
