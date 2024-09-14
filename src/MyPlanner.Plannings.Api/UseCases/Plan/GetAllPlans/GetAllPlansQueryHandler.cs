
using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetAllPlans
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, IEnumerable<PlanDto>>
    {
        private readonly IPlanRepository planRepository;
        private readonly IMapper mapper;

        public GetAllPlansQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PlanDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var entities = await planRepository.GetAllAsync(request.Pagination);

            var dtos = mapper.Map<IEnumerable<PlanDto>>(entities);

            return dtos;
        }
    }
}
