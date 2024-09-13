using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeFactorQueryHandler : IRequestHandler<GetSizeModelTypeFactorQuery, SizeModelTypeFactorDto>
    {
        private readonly ISizeModelTypeFactorRepository sizeModelTypeFactorRepository;
        private readonly IMapper mapper;

        public GetSizeModelTypeFactorQueryHandler(ISizeModelTypeFactorRepository sizeModelTypeFactorRepository, IMapper mapper)
        {
            this.sizeModelTypeFactorRepository = sizeModelTypeFactorRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelTypeFactorDto> Handle(GetSizeModelTypeFactorQuery request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeFactorRepository.GetFactorById(request.SizeModelTypeFactorId);

            var dto = mapper.Map<SizeModelTypeFactorDto>(entity);

            return dto;
        }
    }
}
