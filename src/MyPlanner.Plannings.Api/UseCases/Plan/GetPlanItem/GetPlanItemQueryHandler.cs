using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetPlanItem
{
    public class GetPlanItemQueryHandler : IRequestHandler<GetPlanItemQuery, PlanDto>
    {
        private readonly IPlanItemRepository planItemRepository;
        private readonly IMapper mapper;

        public GetPlanItemQueryHandler(IPlanItemRepository planItemRepository, IMapper mapper)
        {
            this.planItemRepository = planItemRepository ?? throw new ArgumentNullException(nameof(planItemRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PlanDto> Handle(GetPlanItemQuery request, CancellationToken cancellationToken)
        {
            var query = await planItemRepository.GetByIdAsync(request.PlanId, request.PlanItemId);

            var dto = mapper.Map<PlanDto>(query);

            return dto;
        }
    }
}
