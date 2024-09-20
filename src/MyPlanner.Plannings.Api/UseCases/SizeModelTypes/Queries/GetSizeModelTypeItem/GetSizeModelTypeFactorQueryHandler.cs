using MyPlanner.Plannings.Api.Dtos.SizeModelType;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeFactorQueryHandler : IRequestHandler<GetSizeModelTypeItemQuery, SizeModelTypeItemDto>
    {
        private readonly ISizeModelTypeQueryRepository modelTypeQueryRepository;
        private readonly IMapper mapper;

        public GetSizeModelTypeFactorQueryHandler(ISizeModelTypeQueryRepository modelTypeQueryRepository, IMapper mapper)
        {
            this.modelTypeQueryRepository = modelTypeQueryRepository ?? throw new ArgumentNullException(nameof(modelTypeQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelTypeItemDto> Handle(GetSizeModelTypeItemQuery request, CancellationToken cancellationToken)
        {
            var entity = await modelTypeQueryRepository.GetItemById(request.SizeModelTypeItemId);

            var dto = mapper.Map<SizeModelTypeItemDto>(entity);

            return dto;
        }
    }
}
