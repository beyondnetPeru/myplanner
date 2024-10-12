

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeByCode
{
    public class GetSizeModelTypeByCodeQueryHandler : AbstractQueryHandler<GetSizeModelTypeByCodeQuery, ResultSet>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeRepository;

        public GetSizeModelTypeByCodeQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeRepository, ILogger<GetSizeModelTypeByCodeQueryHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleQuery(GetSizeModelTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            var data = await sizeModelTypeRepository.GetByCode(request.Code);

            return ResultSet.Success(data);
        }
    }
}
