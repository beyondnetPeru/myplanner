using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeFactorQueryHandler : AbstractQueryHandler<GetSizeModelTypeItemQuery, ResultSet>
    {
        private readonly ISizeModelTypeQueryRepository modelTypeQueryRepository;
        private readonly IMapper mapper;

        public GetSizeModelTypeFactorQueryHandler(ISizeModelTypeQueryRepository modelTypeQueryRepository, IMapper mapper, ILogger<GetSizeModelTypeFactorQueryHandler> logger): base(logger)
        {
            this.modelTypeQueryRepository = modelTypeQueryRepository ?? throw new ArgumentNullException(nameof(modelTypeQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleQuery(GetSizeModelTypeItemQuery request, CancellationToken cancellationToken)
        {
            var entity = await modelTypeQueryRepository.GetItemById(request.SizeModelTypeItemId);

            var dto = mapper.Map<SizeModelTypeItemDto>(entity);

            return ResultSet.Success(dto);
        }
    }
}
