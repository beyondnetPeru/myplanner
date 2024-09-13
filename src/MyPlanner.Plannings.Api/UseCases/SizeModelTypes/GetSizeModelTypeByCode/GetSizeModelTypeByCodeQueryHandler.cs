using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeByCode
{
    public class GetSizeModelTypeByCodeQueryHandler : IRequestHandler<GetSizeModelTypeByCodeQuery, SizeModelDto>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly IMapper mapper;

        public GetSizeModelTypeByCodeQueryHandler(ISizeModelTypeRepository sizeModelTypeRepository, IMapper mapper)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelDto> Handle(GetSizeModelTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetByCode(request.Code);

            var dto = mapper.Map<SizeModelDto>(sizeModelType);

            return dto;
        }
    }
}
