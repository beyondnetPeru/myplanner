using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetSizeModel
{
    public class GetSizeModelQueryHandler : IRequestHandler<GetSizeModelQuery, SizeModelDto>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly IMapper mapper;

        public GetSizeModelQueryHandler(ISizeModelRepository sizeModelRepository, IMapper mapper)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelDto> Handle(GetSizeModelQuery request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            var dto = mapper.Map<SizeModelDto>(sizeModel);

            return dto;
        }
    }
}
