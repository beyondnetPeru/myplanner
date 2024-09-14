using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdQueryHandler : IRequestHandler<GetSizeModelTypeByIdQuery, SizeModelDto>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;
        private readonly IMapper mapper;

        public GetSizeModelTypeByIdQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository, IMapper mapper)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelDto> Handle(GetSizeModelTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeQueryRepository.GetById(request.Id);

            var dto = mapper.Map<SizeModelDto>(sizeModelType);

            return dto;
        }
    }
}
