using MyPlanner.Plannings.Api.Dtos.SizeModel;


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModel
{
    public class GetSizeModelQueryHandler : IRequestHandler<GetSizeModelQuery, SizeModelDto>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;
        private readonly IMapper mapper;

        public GetSizeModelQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, IMapper mapper)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelDto> Handle(GetSizeModelQuery request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelQueryRepository.Get(request.SizeModelId);

            var dto = mapper.Map<SizeModelDto>(sizeModel);

            return dto;
        }
    }
}
