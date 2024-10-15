

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModels
{
    public class GetAllSizeModelsQueryHandler : AbstractQueryHandler<GetAllSizeModelsQuery, ResultSet>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;

        public GetAllSizeModelsQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, ILogger<GetAllSizeModelsQueryHandler> logger) : base(logger)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
        }

        public override async Task<ResultSet> HandleQuery(GetAllSizeModelsQuery request, CancellationToken cancellationToken)
        {
            var data = await sizeModelQueryRepository.GetAll(request.Pagination);

            return ResultSet.Success(data);
        }
    }
}
