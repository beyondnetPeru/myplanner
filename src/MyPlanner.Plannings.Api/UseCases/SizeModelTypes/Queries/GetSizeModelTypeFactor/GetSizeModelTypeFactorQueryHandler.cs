using MyPlanner.Plannings.Api.Dtos.SizeModelType;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeFactorQueryHandler : IRequestHandler<GetSizeModelTypeFactorQuery, SizeModelTypeFactorDto>
    {
        private readonly ISizeModelTypeQueryRepository modelTypeQueryRepository;
        private readonly IMapper mapper;

        public GetSizeModelTypeFactorQueryHandler(ISizeModelTypeQueryRepository modelTypeQueryRepository, IMapper mapper)
        {
            this.modelTypeQueryRepository = modelTypeQueryRepository ?? throw new ArgumentNullException(nameof(modelTypeQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelTypeFactorDto> Handle(GetSizeModelTypeFactorQuery request, CancellationToken cancellationToken)
        {
            var entity = await modelTypeQueryRepository.GetFactorById(request.SizeModelTypeFactorId);

            var dto = mapper.Map<SizeModelTypeFactorDto>(entity);

            return dto;
        }
    }
}
