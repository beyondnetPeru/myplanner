using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetAllPlanItems
{
    public class GetAllPlanItemsQueryHandler : IRequestHandler<GetAllPlanItemsQuery, IEnumerable<PlanItemDto>>
    {
        private readonly IPlanItemRepository planItemRepository;
        private readonly IMapper mapper;

        public GetAllPlanItemsQueryHandler(IPlanItemRepository planItemRepository, IMapper mapper)
        {
            this.planItemRepository = planItemRepository ?? throw new ArgumentNullException(nameof(planItemRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PlanItemDto>> Handle(GetAllPlanItemsQuery request, CancellationToken cancellationToken)
        {
            var planItems = await planItemRepository.GetAllAsync(request.PlanId);

            var dtos = mapper.Map<IEnumerable<PlanItemDto>>(planItems);

            return dtos;
        }
    }
}
