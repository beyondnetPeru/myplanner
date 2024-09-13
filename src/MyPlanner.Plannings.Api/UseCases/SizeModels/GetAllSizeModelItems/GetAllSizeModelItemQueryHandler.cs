using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetAllSizeModelItems
{
    public class GetAllSizeModelItemQueryHandler : IRequestHandler<GetAllSizeModelItemQuery, IEnumerable<SizeModelItemDto>>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelItemQueryHandler(ISizeModelRepository sizeModelRepository, IMapper mapper)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelItemDto>> Handle(GetAllSizeModelItemQuery request, CancellationToken cancellationToken)
        {
            var sizeModelItems = await sizeModelRepository.GetItems(request.SizeModelId);

            var sizeModelItemDtos = mapper.Map<IEnumerable<SizeModelItemDto>>(sizeModelItems);

            return sizeModelItemDtos;
        }
    }
}
