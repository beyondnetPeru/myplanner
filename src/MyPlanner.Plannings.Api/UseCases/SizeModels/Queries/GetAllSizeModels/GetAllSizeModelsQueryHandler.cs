using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModels
{
    public class GetAllSizeModelsQueryHandler : IRequestHandler<GetAllSizeModelsQuery, IEnumerable<SizeModelDto>>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelsQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository, IMapper mapper)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelDto>> Handle(GetAllSizeModelsQuery request, CancellationToken cancellationToken)
        {
            var sizeModels = await sizeModelQueryRepository.GetAll(request.Pagination);

            var sizeModelsDto = mapper.Map<IEnumerable<SizeModelDto>>(sizeModels);

            return sizeModelsDto;
        }
    }
}
