using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModelItems
{
    public class GetAllSizeModelItemQueryHandler : IRequestHandler<GetAllSizeModelItemQuery, IEnumerable<SizeModelItemDto>>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelItemQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, IMapper mapper)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelItemDto>> Handle(GetAllSizeModelItemQuery request, CancellationToken cancellationToken)
        {
            var sizeModelItems = await sizeModelQueryRepository.GetItems(request.SizeModelId);

            var sizeModelItemDtos = mapper.Map<IEnumerable<SizeModelItemDto>>(sizeModelItems);

            return sizeModelItemDtos;
        }
    }
}
