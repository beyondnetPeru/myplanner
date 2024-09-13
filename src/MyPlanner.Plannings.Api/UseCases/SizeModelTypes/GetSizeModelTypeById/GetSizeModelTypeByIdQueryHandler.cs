using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdQueryHandler : IRequestHandler<GetSizeModelTypeByIdQuery, SizeModelDto>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly IMapper mapper;

        public GetSizeModelTypeByIdQueryHandler(ISizeModelTypeRepository sizeModelTypeRepository, IMapper mapper)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelDto> Handle(GetSizeModelTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetById(request.Id);

            var dto = mapper.Map<SizeModelDto>(sizeModelType);

            return dto;
        }
    }
}
