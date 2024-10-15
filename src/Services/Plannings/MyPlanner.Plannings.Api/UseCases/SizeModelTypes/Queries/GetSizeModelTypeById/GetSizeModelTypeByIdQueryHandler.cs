

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdQueryHandler : AbstractQueryHandler<GetSizeModelTypeByIdQuery, ResultSet>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;

        public GetSizeModelTypeByIdQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository, ILogger<GetSizeModelTypeByIdQueryHandler> logger) : base(logger)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
        }

        public override async Task<ResultSet> HandleQuery(GetSizeModelTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await sizeModelTypeQueryRepository.GetById(request.Id);

            return ResultSet.Success(data);
        }
    }
}
