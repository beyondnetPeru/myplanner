using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlanItems
{
    public class GetAllPlanItemsQueryHandler : IRequestHandler<GetAllPlanItemsQuery, IEnumerable<PlanItemDto>>
    {
        private readonly IPlanQueryRepository planQueryRepository;
        private readonly IMapper mapper;

        public GetAllPlanItemsQueryHandler(IPlanQueryRepository planQueryRepository, IMapper mapper)
        {
            this.planQueryRepository = planQueryRepository ?? throw new ArgumentNullException(nameof(planQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PlanItemDto>> Handle(GetAllPlanItemsQuery request, CancellationToken cancellationToken)
        {
            var planItems = await planQueryRepository.GetPlanItems(request.PlanId);

            var dtos = mapper.Map<IEnumerable<PlanItemDto>>(planItems);

            return dtos;
        }
    }
}
