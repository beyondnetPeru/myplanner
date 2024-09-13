using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetSizeModelItem
{
    public class GetSizeModelItemQueryHandler : IRequestHandler<GetSizeModelItemQuery, SizeModelItemDto>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly IMapper mapper;

        public GetSizeModelItemQueryHandler(ISizeModelRepository sizeModelRepository, IMapper mapper)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<SizeModelItemDto> Handle(GetSizeModelItemQuery request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelId, request.SizeModelItemId);

            var dto = mapper.Map<SizeModelItemDto>(sizeModelItem);

            return dto;
        }
    }
}
