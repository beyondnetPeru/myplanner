using MyPlanner.Shared.Cqrs;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeItemsQueryHandler : AbstractQueryHandler<GetAllSizeModelTypeItemsQuery, ResultSet>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;

        public GetAllSizeModelTypeItemsQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository, ILogger<GetAllSizeModelTypeItemsQueryHandler> logger) : base(logger)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
        }

        public override async Task<ResultSet> HandleQuery(GetAllSizeModelTypeItemsQuery request, CancellationToken cancellationToken)
        {
            var data = await sizeModelTypeQueryRepository.GetItems(request.SizeModelTypeId);

            return ResultSet.Success(data);
        }
    }
}
