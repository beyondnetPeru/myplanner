namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlanByCode
{
    public class GetPlanByCodeQueryHandler : AbstractQueryHandler<GetPlanByCodeQuery, ResultSet>
    {
        private readonly IPlanQueryRepository planQueryRepository;

        public GetPlanByCodeQueryHandler(IPlanQueryRepository planQueryRepository, ILogger<AbstractQueryHandler<GetPlanByCodeQuery, ResultSet>> logger) : base(logger)
        {
            this.planQueryRepository = planQueryRepository ?? throw new ArgumentNullException(nameof(planQueryRepository));
        }

        public async override Task<ResultSet> HandleQuery(GetPlanByCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await planQueryRepository.GetPlanByCode(request.Code);

            if (result == null)
            {
                return ResultSet.Error("Plan not found");
            }

            return ResultSet.Success(result);
        }
    }
}
