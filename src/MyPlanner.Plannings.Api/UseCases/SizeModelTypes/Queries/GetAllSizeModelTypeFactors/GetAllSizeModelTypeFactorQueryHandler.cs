using MyPlanner.Plannings.Api.Dtos.SizeModelType;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeFactorQueryHandler : IRequestHandler<GetAllSizeModelTypeFactorQuery, IEnumerable<SizeModelTypeFactorDto>>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelTypeFactorQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository, IMapper mapper)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelTypeFactorDto>> Handle(GetAllSizeModelTypeFactorQuery request, CancellationToken cancellationToken)
        {
            var sizeModelTypeFactors = await sizeModelTypeQueryRepository.GetFactors(request.SizeModelTypeId);

            var dtos = mapper.Map<IEnumerable<SizeModelTypeFactorDto>>(sizeModelTypeFactors);

            return dtos;
        }
    }
}
