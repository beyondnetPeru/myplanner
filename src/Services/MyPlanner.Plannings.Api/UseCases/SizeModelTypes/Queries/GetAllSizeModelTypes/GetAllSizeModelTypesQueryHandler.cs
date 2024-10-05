using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypes
{
    public class GetAllSizeModelTypesQueryHandler : AbstractQueryHandler<GetAllSizeModelTypesQuery, ResultSet>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;

        public GetAllSizeModelTypesQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository, ILogger<GetAllSizeModelTypesQueryHandler> logger) : base(logger)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
        }

        public override async Task<ResultSet> HandleQuery(GetAllSizeModelTypesQuery request, CancellationToken cancellationToken)
        {
            var data = await sizeModelTypeQueryRepository.GetAll(request.Pagination);

            return ResultSet.Success(data);
        }
    }
}
