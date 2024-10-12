


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModel
{
    public class GetSizeModelQueryHandler : AbstractQueryHandler<GetSizeModelQuery, ResultSet>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;

        public GetSizeModelQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, ILogger<GetSizeModelQueryHandler> logger): base(logger) 
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
        }

        public override async Task<ResultSet> HandleQuery(GetSizeModelQuery request, CancellationToken cancellationToken)
        {
            var data = await sizeModelQueryRepository.Get(request.SizeModelId);

            return ResultSet.Success(data);
        }
    }
}
