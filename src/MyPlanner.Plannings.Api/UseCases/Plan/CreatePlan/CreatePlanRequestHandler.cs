using AutoMapper;
using BeyondNet.Ddd.ValueObjects;
using MediatR;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.CreatePlan
{
    public class CreatePlanRequestHandler : IRequestHandler<CreatePlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreatePlanRequestHandler> logger;

        public CreatePlanRequestHandler(IPlanRepository planRepository, IMapper mapper, ILogger<CreatePlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreatePlanRequest request, CancellationToken cancellationToken)
        {
            var props = mapper.Map<PlanProps>(request);

            var plan = Domain.PlanAggregate.Plan.Create(IdValueObject.Create(), props.SizeModelTypeId, props.SizeModelTypeName, props.Name, props.Owner, UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Invalid plan. Errors: {plan.GetBrokenRules()}");
                return false;
            }

            await planRepository.AddAsync(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
