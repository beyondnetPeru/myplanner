
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModelItems
{
    public class GetAllSizeModelItemQueryHandler : AbstractQueryHandler<GetAllSizeModelItemQuery, ResultSet>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelItemQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, IMapper mapper, ILogger<GetAllSizeModelItemQueryHandler> logger) : base(logger)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleQuery(GetAllSizeModelItemQuery request, CancellationToken cancellationToken)
        {
            var sizeModelItems = await sizeModelQueryRepository.GetItems(request.SizeModelId);

            var sizeModelItemDtos = mapper.Map<IEnumerable<SizeModelItemDto>>(sizeModelItems);

            return ResultSet.Success(sizeModelItemDtos);
        }
    }
}
