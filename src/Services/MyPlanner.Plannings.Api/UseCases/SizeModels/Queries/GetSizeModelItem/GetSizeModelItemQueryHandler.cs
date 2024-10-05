using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModelItem
{
    public class GetSizeModelItemQueryHandler : AbstractQueryHandler<GetSizeModelItemQuery, ResultSet>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;
        private readonly IMapper mapper;

        public GetSizeModelItemQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, IMapper mapper, ILogger<GetSizeModelItemQueryHandler> logger) : base(logger)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleQuery(GetSizeModelItemQuery request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelQueryRepository.GetItem(request.SizeModelItemId);

            var dto = mapper.Map<SizeModelItemDto>(sizeModelItem);

            return ResultSet.Success(dto);
        }
    }
}
