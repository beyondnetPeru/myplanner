using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModelItem
{
    public class GetSizeModelItemQueryHandler : IRequestHandler<GetSizeModelItemQuery, SizeModelItemDto>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;
        private readonly IMapper mapper;

        public GetSizeModelItemQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, IMapper mapper)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<SizeModelItemDto> Handle(GetSizeModelItemQuery request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelQueryRepository.GetItem(request.SizeModelId, request.SizeModelItemId);

            var dto = mapper.Map<SizeModelItemDto>(sizeModelItem);

            return dto;
        }
    }
}
