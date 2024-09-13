using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetAllSizeModels
{
    public class GetAllSizeModelsQueryHandler : IRequestHandler<GetAllSizeModelsQuery, IEnumerable<SizeModelDto>>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelsQueryHandler(ISizeModelRepository sizeModelRepository, IMapper mapper)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelDto>> Handle(GetAllSizeModelsQuery request, CancellationToken cancellationToken)
        {
            var sizeModels = await sizeModelRepository.GetAll(request.Pagination);

            var sizeModelsDto = mapper.Map<IEnumerable<SizeModelDto>>(sizeModels);

            return sizeModelsDto;
        }
    }
}
