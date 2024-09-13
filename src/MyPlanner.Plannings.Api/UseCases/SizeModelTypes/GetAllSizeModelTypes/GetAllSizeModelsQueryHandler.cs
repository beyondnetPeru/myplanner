using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetAllSizeModelTypes
{
    public class GetAllSizeModelsQueryHandler : IRequestHandler<GetAllSizeModelsQuery, IEnumerable<SizeModelDto>>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelsQueryHandler(ISizeModelTypeRepository sizeModelTypeRepository, IMapper mapper)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelDto>> Handle(GetAllSizeModelsQuery request, CancellationToken cancellationToken)
        {
            var sizeModels = await sizeModelTypeRepository.GetAll(request.Pagination);

            var dtos = mapper.Map<IEnumerable<SizeModelDto>>(sizeModels);

            return dtos;
        }
    }
}
