using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, IEnumerable<PlanDto>>
    {
        private readonly IPlanQueryRepository planQueryRepository;
        private readonly IMapper mapper;

        public GetAllPlansQueryHandler(IPlanQueryRepository planQueryRepository, IMapper mapper)
        {
            this.planQueryRepository = planQueryRepository ?? throw new ArgumentNullException(nameof(planQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PlanDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var entities = await planQueryRepository.GetPlans(request.Pagination);

            var dtos = mapper.Map<IEnumerable<PlanDto>>(entities);

            return dtos;
        }
    }
}
